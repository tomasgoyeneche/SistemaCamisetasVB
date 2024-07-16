Imports System.Threading.Tasks
Imports System.IO
Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Net.Mail
Imports System.Security.Cryptography
Public Class _Default
    Inherits System.Web.UI.Page
    Public x As Integer
    Public connectionstring As String = ConfigurationManager.ConnectionStrings(ConfigurationManager.AppSettings("Conexion")).ToString()
    Public EmailActivo As String = ConfigurationManager.AppSettings("EmailActivo")
    Public Email As String = ConfigurationManager.AppSettings(EmailActivo)
    Public EmailPass As String = ConfigurationManager.AppSettings(EmailActivo & "Pass")

    Dim con As New SqlConnection(connectionstring)
    Dim ar As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub


    ''FUNCIONES VARIAS
    Sub ArreglarCampo(ByRef campo As TextBox)
        campo.Text = campo.Text.Trim.Replace("'", "").Replace("""",
       "").Replace("*", "").Replace("+", "").Replace("-", "").Replace("/",
       "").Replace(":", "").Replace("`", "").Replace("<", "").Replace(">",
       "").Replace("=", "").Replace("&", "")
    End Sub

    Function comprobar(ByVal user As String) As Boolean
        If user Is System.DBNull.Value Then
            Return False
        Else
            Dim aux As Boolean = True
            Dim listanegra, x As String
            listanegra = "'*!-/;:><" & """"
            If user <> "" Then
                For Each x In user
                    If aux = True Then
                        If InStr(1, listanegra, x) > 0 Then
                            aux = False
                        Else
                            aux = True
                        End If
                    Else
                        Return False
                    End If
                Next
                If aux = True Then
                    Return True
                Else
                    Return False
                End If
            Else
                Return False
            End If
        End If
    End Function

    Public Function ValidateEmail(ByVal email As String) As Boolean
        Dim emailRegex As New System.Text.RegularExpressions.Regex("^(?<user>[^@]+)@(?<host>.+)$")
        Dim emailMatch As System.Text.RegularExpressions.Match =
        emailRegex.Match(email)
        Return emailMatch.Success
    End Function

    Protected Sub SoloNumeros(ByRef campo As TextBox)
        Dim cam As String = campo.Text.Trim, salida As String = "", c As String
        For Each c In cam
            If IsNumeric(c) Then salida &= c
        Next
        campo.Text = salida
    End Sub

    Function VNum(ByVal NTexto As String) As Decimal
        Return CDec(Val(NTexto.Trim.Replace(",", ".")))

    End Function

    Function NumSQL(ByVal numero As String) As String
        Return CStr(VNum(numero)).Trim.Replace(",", ".")
    End Function

    Function RellenaNum(ByVal numero As Integer, ByVal cantidad As Integer) As String
        Dim snum As String = CStr(numero).Trim
        Return "00000000000000000000".Substring(0, cantidad - snum.Length) & snum
    End Function

    Function FechaSQL(ByVal fecha As Date) As String
        Return "'" & RellenaNum(Year(fecha), 4) & RellenaNum(Month(fecha), 2) & RellenaNum(fecha.Day, 2) & "'"
    End Function

    Public Function AnioMes(ByVal fecha As Date) As String
        Return RellenaNum(Year(fecha), 4) & "-" & RellenaNum(Month(fecha), 2)
    End Function

    Public Function Vsrt(ByVal cosa As Object) As String
        If IsDBNull(cosa) Then Return "" Else Return CStr(cosa)
    End Function

    Function CalcularEdad(ByVal FechaNac As Date) As Integer
        Dim edad As Integer = DateTime.Today.AddTicks(-FechaNac.Ticks).Year - 1
        Return edad
    End Function

    Sub ControlDeNacimiento(ByRef xtf_nac As TextBox, ByRef ok As Boolean,
                            ByRef xlEFNac As Label, ByVal xlEdad As Label, ByVal Valid18 As Boolean, ByRef FechaNacimiento As Date)

        ArreglarCampo(xtf_nac)
        xlEdad.Visible = False
        If xtf_nac.Text.Length < 6 Then
            ok = False
            xlEFNac.Text = "El campo fecha de nacimiento debe tener 6 números"
            xlEdad.Text = "0"
            xlEFNac.Visible = True
        Else
            Dim FechaX As String = xtf_nac.Text
            Dim AñoX As Integer = VNum(FechaX.Substring(4, 2))
            If AñoX + 2000 > Date.Today.Year Then AñoX += 1900 Else AñoX += 2000
            FechaX = AñoX.ToString.Trim & "-" & FechaX.Substring(2, 2) & "-" & FechaX.Substring(0, 2)
            If Not IsDate(FechaX) Then
                ok = False
                xlEFNac.Text &= "El campo fecha de nacimiento es una fecha inválida."
                xlEdad.Text = "0"
                xlEFNac.Visible = True
            Else
                Dim dFechaX As Date
                dFechaX = CDate(FechaX)
                FechaNacimiento = dFechaX
                If dFechaX > Date.Today Then
                    ok = False
                    xlEFNac.Text &= "Nació en el futuro...."
                    xlEdad.Text = "0"
                    xlEFNac.Visible = True
                Else
                    Dim Edad As Integer = CalcularEdad(dFechaX)
                    xlEdad.Text = Edad
                    If Edad < 18 And Valid18 Then
                        ok = False
                        xlEFNac.Text &= "Debe ser mayor de 18 años."
                        xlEdad.Text = "0"
                        xlEFNac.Visible = True
                    End If
                End If
            End If
        End If
    End Sub

    Public Function SQL_Accion(ByVal sql_de_accion As String) As Boolean
        ' Ejecuta la sentencia sql 'sql_de_accion' abriendo la conexión automáticamente.
        ' Se da cuenta si es de insert, update o delete, porque mira dentro de la sentencia que se le pasa.
        ' Devuelve la respuesta obtenida y false si hubo algún error.
        Dim adapter As New SqlDataAdapter, salida As Boolean = True

        Try
            If con.State = ConnectionState.Closed Then con.Open()


            If sql_de_accion.ToUpper.IndexOf("INSERT") Then
                adapter.InsertCommand = New SqlCommand(sql_de_accion, con)
                adapter.InsertCommand.ExecuteNonQuery()
            Else
                If sql_de_accion.ToUpper.IndexOf("UPDATE") Then
                    adapter.UpdateCommand = New SqlCommand(sql_de_accion, con)
                    adapter.UpdateCommand.ExecuteNonQuery()
                Else
                    If sql_de_accion.ToUpper.IndexOf("DELETE") Then
                        adapter.DeleteCommand = New SqlCommand(sql_de_accion, con)
                        adapter.DeleteCommand.ExecuteNonQuery()
                    Else
                        ' Esta mal la sintaxis porque no hay ni insert; ni delete; ni update.
                        salida = False
                    End If
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
            salida = False
        End Try

        Return salida
    End Function

    Function LeeUnCampo(ByVal sql As String, ByVal campo As String) As Object
        Dim da1 As New SqlDataAdapter(sql, con)
        Dim ds1 As New DataSet
        Try
            If con.State = ConnectionState.Closed Then con.Open()
            da1.Fill(ds1, "datos")
            If ds1.Tables("datos").Rows.Count < 1 Then
                Return "**NADA**"
            Else
                Return ds1.Tables("datos").Rows(0)(campo)
            End If
        Catch
            Return "**ERROR**"
        End Try
    End Function

    Public Function YaExisteSQL(ByVal sql As String) As Boolean
        Dim con As New SqlConnection(connectionstring)
        Dim da1 As New SqlDataAdapter(sql, con)
        Dim ds1 As New DataSet
        da1.Fill(ds1, "afidesc")
        If ds1.Tables("afidesc").Rows.Count < 1 Then
            Return False
        Else
            Return True
        End If
    End Function







    '' Limpiar errores registro 

    '' Registro de Editar Empleado o Admin
    Sub LimpiarErroresEditU()
        cError.Text = ""
        cError.Visible = False
        clEEmailU.Text = ""
        clELocalidadU.Text = ""
        clEDireccionU.Text = ""
        clETelefonosU.Text = ""
        clEDocU.Text = ""
        clEUsuarioU.Text = ""
        clEPassU.Text = ""
        clEPassU.Visible = False
        clEUsuarioU.Visible = False
        clEDocU.Visible = False
        clETelefonosU.Visible = False
        clEDireccionU.Visible = False
        clELocalidadU.Visible = False
        clEEmailU.Visible = False
    End Sub

    Sub LimpiarRegistroEditU()
        LimpiarErroresEditU()

        cdProvinciaU.SelectedIndex = 0
        ctEmailU.Text = ""
        ctLocalidadU.Text = ""
        ctDireccionU.Text = ""
        ctTelefonosU.Text = ""
        ctDocU.Text = ""
        ctUsuarioU.Text = ""
        ctPassU.Text = ""
    End Sub



    '' Limpiar errores registro del register y login

    Sub LimpiarErroresRegistroU()
        lErroresU.Text = ""
        lErroresU.Visible = False
        lEFNac.Visible = False
        lEFNac.Text = ""
        lENombreU.Text = ""
        lEApellidoU.Text = ""
        lEDocU.Text = ""
        lEEmailU.Text = ""
        lELocalidadU.Text = ""
        lEDireccionU.Text = ""
        lETelefonosU.Text = ""
        lEUsuarioU.Text = ""
        lEPassU.Text = ""
        lEPass2U.Text = ""
        lENombreU.Visible = False
        lEApellidoU.Visible = False
        lEDocU.Visible = False
        lEEmailU.Visible = False
        lELocalidadU.Visible = False
        lEDireccionU.Visible = False
        lETelefonosU.Visible = False
        lEUsuarioU.Visible = False
        lEPassU.Visible = False
        lEPass2U.Visible = False
    End Sub
    Sub LimpiarRegistroU()
        LimpiarErroresRegistroU()
        pRegistrarse.Visible = False

        tNombreU.Text = ""
        tApellidoU.Text = ""
        dTDocU.SelectedIndex = 0
        tDocU.Text = ""
        tEmailU.Text = ""
        didProvU.SelectedIndex = 0
        tLocalidadU.Text = ""
        tDireccionU.Text = ""
        tTelefonosU.Text = ""
        tUsuarioU.Text = ""
        tPassU.Text = ""
        tPass2U.Text = ""
    End Sub

    '' Limpiar Login

    Sub LimpiarLogin()
        lErrorLogin.Text = ""
        lErrorLogin.Visible = False
        tUsuario.Text = ""
        tClave.Text = ""
        pRegistrarse.Visible = False
    End Sub



    '' Registro de Usuario Buttons

    Protected Sub bRegistrarseBack_Click(sender As Object, e As EventArgs)
        LimpiarRegistroU()
        pLoginMenu.Visible = True
        pRegistrarse.Visible = False
    End Sub

    Protected Sub bRegistrarseSuccess_Click(sender As Object, e As EventArgs)
        Dim ok As Boolean = True

        LimpiarErroresRegistroU()

        tNombreU.Text = tNombreU.Text.Trim.ToUpper
        If comprobar(tNombreU.Text) = False Then
            ArreglarCampo(tNombreU)
            ok = False
            lENombreU.Text = "El nombre contenia caracteres invalidos, fueron quitados."
            lENombreU.Visible = True
        End If

        tApellidoU.Text = tApellidoU.Text.Trim.ToUpper
        If comprobar(tApellidoU.Text) = False Then
            ArreglarCampo(tApellidoU)
            ok = False
            lEApellidoU.Text = "El apellido contenia caracteres invalidos, fueron quitados."
            lEApellidoU.Visible = True
        End If

        tDocU.Text = tDocU.Text.Trim
        If comprobar(tDocU.Text) = False Or Not IsNumeric(tDocU.Text) Then
            SoloNumeros(tDocU)
            ok = False
            lEDocU.Text = "El documento no era valido, se ajusto a numero."
            lEDocU.Visible = True
        End If

        ArreglarCampo(tEmailU)
        If ValidateEmail(tEmailU.Text) = False Then
            ok = False
            lEEmailU.Text = "El mail no es valido."
            lEEmailU.Visible = True
        End If

        tLocalidadU.Text = tLocalidadU.Text.Trim().ToUpper
        If comprobar(tLocalidadU.Text) = False Then
            ArreglarCampo(tLocalidadU)
            ok = False
            lELocalidadU.Text = "La Localidad contenía caracteres inválidos, fueron quitados."
            lELocalidadU.Visible = True
        End If

        tDireccionU.Text = tDireccionU.Text.Trim().ToUpper
        If comprobar(tDireccionU.Text) = False Then
            ArreglarCampo(tDireccionU)
            ok = False
            lEDireccionU.Text = "La Dirección contenía caracteres inválidos, fueron quitados."
            lEDireccionU.Visible = True
        End If

        tTelefonosU.Text = tTelefonosU.Text.Trim().ToUpper
        If comprobar(tTelefonosU.Text) = False Then
            ArreglarCampo(tTelefonosU)
            ok = False
            lETelefonosU.Text = "El teléfono contenía caracteres inválidos, fueron quitados."
            lETelefonosU.Visible = True
        End If

        Dim FechaNacimiento As Date
        ControlDeNacimiento(tF_Nac, ok, lEFNac, lEdad, True, FechaNacimiento)

        tUsuarioU.Text = tUsuarioU.Text.Trim().ToUpper()
        If comprobar(tUsuarioU.Text) = False Or tUsuarioU.Text.IndexOf(" ") > -1 Then
            tUsuarioU.Text = tUsuarioU.Text.Replace(" ", "")
            ArreglarCampo(tUsuarioU)
            ok = False
            lEUsuarioU.Text = "El usuario contenía caracteres inválidos, fueron quitados."
            lEUsuarioU.Visible = True
        End If

        If tUsuarioU.Text.Length < 5 Then
            ok = False
            lEUsuarioU.Text = "El usuario debe tener de 5 a 10 caracteres, letras y/o números."
            lEUsuarioU.Visible = True
        End If

        tPassU.Text = tPassU.Text.Trim()
        If comprobar(tPassU.Text) = False Or tPassU.Text.IndexOf(" ") > -1 Then
            tPassU.Text = tPassU.Text.Replace(" ", "")
            ArreglarCampo(tPassU)
            ok = False
            lEPassU.Text = "la clave contenía caracteres inválidos. Pruebe con letras y números."
            lEPassU.Visible = True
        End If

        If tPassU.Text.Length < 5 Then
            ok = False
            lEPassU.Text = "la clave debe tener de 5 a 10 caracteres, letras y/o números."
            lEPassU.Visible = True
        End If

        tPass2U.Text = tPass2U.Text.Trim()
        If comprobar(tPass2U.Text) = False Or tPass2U.Text.IndexOf(" ") > -1 Then
            tPass2U.Text = tPass2U.Text.Replace(" ", "")
            ArreglarCampo(tPass2U)
            ok = False
            lEPass2U.Text = "La segunda clave contenía caracteres inválidos. Pruebe con letras y números."
            lEPass2U.Visible = True
        End If
        If ok = False Then
            lErroresU.Text = "Revise los errores por favor y luego reintente."
            lErroresU.Visible = True
            Exit Sub
        End If
        If tPass2U.Text <> tPassU.Text Then
            ok = False
            lEPass2U.Text = "las 2 claves son diferentes."
            lEPass2U.Visible = True
        End If

        Session("Usuario") = tUsuarioU.Text
        Session("Password") = tPassU.Text
        Session("TipoDocumento") = dTDocU.SelectedValue.Trim
        Session("Documento") = tDocU.Text.Trim
        If YaExisteSQL("select idusuario from usuarios where usuario='" & Session("Usuario") & "'") Then
            ok = False
            lEUsuarioU.Text = "El usuario elegido ya existe. Pruebe con otro."
            lEUsuarioU.Visible = True
        End If
        If YaExisteSQL("select idusuario from usuarios where doc='" & Session("Documento") _
        & "' and tdoc='" & Session("TipoDocumento") & "'") Then
            ok = False
            lEDocU.Text = "Ya existe el " & Session("TipoDocumento") & " " & Session("Documento ") & "."
            lEDocU.Visible = True
        End If

        If ok = False Then
            lErroresU.Text = “Solo se permite una inscripción por persona."
            lErroresU.Visible = True
            Exit Sub
        End If

        Session(“Usuario”) = tUsuarioU.Text.ToLower
        Session(“Password”) = tPassU.Text
        Session(“TipoDocumento”) = dTDocU.SelectedValue.Trim
        Session("Documento") = tDocU.Text.Trim
        Session(“ApellidoYNombre”) = tNombreU.Text.Trim & " " & tApellidoU.Text.Trim
        Session(“email”) = tEmailU.Text.Trim

        LimpiarErroresRegistroU()



        '' codigo en caso de ingreso por validacion de mail

        'Session("sqlIngreso") = "insert into usuarios (Apellido, Nombre, tDoc, doc, Usuario, Pass, Email, IdProv, Localidad, Direccion, Telefonos, fNacimiento) values ('" _
        '              & tApellidoU.Text.Trim & "','" & tNombreU.Text.Trim & "','" & Session("TipoDocumento") & "','" _
        '              & Session("Documento") & "','" & Session("Usuario") & "','" & Session("Password") & "','" _
        '              & Session("Email") & "'," & didProvU.SelectedValue & ",'" & tLocalidadU.Text.Trim & "','" _
        '              & tDireccionU.Text.Trim & "','" & tTelefonosU.Text.Trim & "','" & FechaNacimiento.ToString("yyyy-MM-dd") & "')"

        'Dim codigo As String = CreaCodigo(4)
        'Session("Codigo") = codigo

        'Dim en As String = Chr(13) & Chr(10), nensaje As String = "Saludos" &
        '    Session("ApellidoYNombre") & "." & en & en &
        '    "Te escribimos desde camisetas nani, en respuesta a tu solicitud de registro" &
        '    "como nuevo usuario de la app." & en & en &
        '    "Por favor, ingresa el codigo: " & codigo & " en el cuadro de texto de " &
        '    "la aplicacion web, y presiona Validar. Esto completara tu registro " &
        '    "como nuevo usuario de Camisetas Nani " & en & en & "Felicitaciones" & en &
        '    "El equipo de Camisetas Nani" & en

        'Dim ok2 As String = EnviarMail(Session("Email"), "Camisetas Nani- Registro como Usuario", mensaje)
        'tValidar.Text = ""
        'lCodigo.Visible = False
        'pRegistrarse.Visible = False
        'pVerificaMail.Visible = True

        '' Fin del codigo en caso de ingreso por validacion de mail

        If SQL_Accion("insert into usuarios (Apellido, Nombre, tDoc, doc, Usuario, Pass, Email, IdProv, Localidad, Direccion, Telefonos, fNacimiento) values ('" _
                      & tApellidoU.Text.Trim & "','" & tNombreU.Text.Trim & "','" & Session("TipoDocumento") & "','" _
                      & Session("Documento") & "','" & Session("Usuario") & "','" & Session("Password") & "','" _
                      & Session("Email") & "'," & didProvU.SelectedValue & ",'" & tLocalidadU.Text.Trim & "','" _
                      & tDireccionU.Text.Trim & "','" & tTelefonosU.Text.Trim & "','" & FechaNacimiento.ToString("yyyy-MM-dd") & "')") = False Then
            lErroresU.Text = "Se ha producido un error al querer guardar tus datos..."
            lErroresU.Visible = True
            Exit Sub
        End If
        Session("QueEs") = "Usuarios"
        Session("idUsuario") = VNum(LeeUnCampo("select top 1 idUsuario from usuarios where Usuario='" _
             & Session("Usuario") & "' and Pass='" & Session("Password") & "' ", "idUsuario"))
        lBienvenido.Text = "Bienvenido " & Session("ApellidoYNombre") & "!"

        Dim mensaje As String, xusuario As String = Session("ApellidoYNombre"),
            en As String = Chr(13) & Chr(10)
        mensaje = "Bienvenido " & xusuario & " a Camisetas Nani!" & en & en &
              "Te damos una cordial bienvenida a la comunidad Camisetas Nani!" & en & en &
              "Tu usuario es " & """" & Session("Usuario") & """" & en & en &
              "Tu clave es " & """" & Session("Password") & """" & en & en &
              "Ya podes loguearte para ver tus datos!!" & en & en

        LimpiarRegistroU()
        pRegistrarse.Visible = False
        pBienvenido.Visible = True
        bCanVolLoginMenu.Focus()
    End Sub


    '' Volver al login postRegistro

    Protected Sub bOkBack_Click(sender As Object, e As EventArgs)
        pBienvenido.Visible = False
        pLogin.Visible = True
    End Sub


    '' Filtra segun que boton si vas a iniciar como usuario o como admin


    Protected Sub bBotonLogUsu_Click(sender As Object, e As EventArgs)
        Session("QueEs") = "Usuarios"
        pAdminUsuLog.Visible = False
        pLogin.Visible = True
    End Sub

    Protected Sub bBotonLogAdm_Click(sender As Object, e As EventArgs)
        Session("QueEs") = "Administradores"
        pAdminUsuLog.Visible = False
        pLogin.Visible = True
    End Sub


    '' Funcion de logueo de Usuario

    Sub Loguea()
        Dim usu As String = tUsuario.Text.Trim.ToUpper, pass As String = tClave.Text.Trim
        If comprobar(tUsuario.Text.Trim) = False Or comprobar(tClave.Text.Trim) = False Then
            lErrorLogin.Text = "El usuario o la clave son incorrectos. Revise por favor."
            lErrorLogin.Visible = True
            Exit Sub
        End If

        Dim da1 As New SqlDataAdapter("SELECT * FROM " & Session("QueEs") &
                                  " WHERE UPPER(LTRIM(RTRIM(usuario)))='" & usu &
                                  "' AND LTRIM(RTRIM(pass))='" & pass &
                                  "' AND activo=1", con)
        Dim ds1 As New DataSet
        da1.Fill(ds1, "Login")
        If ds1.Tables("Login").Rows.Count = 0 Then
            lErrorLogin.Text = "El usuario o la clave son incorrectos, o el usuario no está activo. Revise por favor."
            lErrorLogin.Visible = True
            Exit Sub
        End If

        Select Case Session("QueEs")
            Case "Usuarios"
                Session("idUsuario") = ds1.Tables("Login").Rows(0)("idUsuario")
                Session("Usuario") = usu
                Session("Password") = pass
                Session("TipoDocumento") = ds1.Tables("Login").Rows(0)("tDoc")
                Session("Documento") = ds1.Tables("Login").Rows(0)("Doc").ToString.Trim
                Session("ApellidoYNombre") = ds1.Tables("Login").Rows(0)("Nombre").ToString.Trim _
                    & " " & ds1.Tables("Login").Rows(0)("Apellido").ToString.Trim
                Session("Email") = ds1.Tables("Login").Rows(0)("Email").ToString.Trim
                Session("idprov") = ds1.Tables("Login").Rows(0)("idprov").ToString.Trim
                Session("Localidad") = ds1.Tables("Login").Rows(0)("Localidad").ToString.Trim
                Session("Direccion") = ds1.Tables("Login").Rows(0)("Direccion").ToString.Trim
                Session("Telefonos") = ds1.Tables("Login").Rows(0)("Telefonos").ToString.Trim
                lBienvenidoAreaU.Text = "Bienvenido " & Session("ApellidoYNombre") & ", a tu Área de Usuario."
                LimpiarRegistroU()
                bNuevoPedido.Visible = True
                bCambioProducto.Visible = False
                bDesEliUsu.Visible = False
                bABMAdmin.Visible = False
                pLogin.Visible = False
                pAreaUsuario.Visible = True
            Case "Administradores"
                Session("idAdmin") = ds1.Tables("Login").Rows(0)("idAdmin")
                Session("Usuario") = usu
                Session("Password") = pass
                Session("TipoDocumento") = ds1.Tables("Login").Rows(0)("tDoc")
                Session("Documento") = ds1.Tables("Login").Rows(0)("Doc").ToString.Trim
                Session("ApellidoYNombre") = ds1.Tables("Login").Rows(0)("Nombre").ToString.Trim _
                    & " " & ds1.Tables("Login").Rows(0)("Apellido").ToString.Trim
                Session("Email") = ds1.Tables("Login").Rows(0)("Email").ToString.Trim
                Session("idprov") = ds1.Tables("Login").Rows(0)("idprov").ToString.Trim
                Session("Localidad") = ds1.Tables("Login").Rows(0)("Localidad").ToString.Trim
                Session("Direccion") = ds1.Tables("Login").Rows(0)("Direccion").ToString.Trim
                Session("Telefonos") = ds1.Tables("Login").Rows(0)("Telefonos").ToString.Trim
                lBienvenidoAreaU.Text = "Bienvenido Administrador " & Session("ApellidoYNombre") & ", a tu Area de Admin."
                LimpiarRegistroU()
                bNuevoPedido.Visible = False
                bCambioProducto.Visible = True
                bDesEliUsu.Visible = True
                bABMAdmin.Visible = True
                pLogin.Visible = False
                pAreaUsuario.Visible = True
                'MostrarCuantosAEliminar()
                'MostrarCuantosUsuariosHay()
                'pAreaAdmin.Visible = True
        End Select

    End Sub

    '' Logueo de Usuario
    Protected Sub Button2_Click(sender As Object, e As EventArgs)
        Loguea()
    End Sub




    ''modificado para camisetas
    Sub CargarHelados()
        Dim x As Integer = 0, cosa As String, cosita As String
        Dim da2 As New SqlDataAdapter("SELECT C.Equipo, P.Nombre AS Pais FROM web_camisetas C JOIN Paises P ON C.idPais = P.idPais ORDER BY C.Equipo", con)
        Dim ds2 As New DataSet
        dCamisetas.Items.Clear()
        dPais.Items.Clear()
        da2.Fill(ds2, "dato")

        If ds2.Tables("dato").Rows.Count = 0 Then
            dCamisetas.Items.Add("*** No hay Camisetas disponibles en este momento... ***")
            Exit Sub
        End If

        Dim paisesAgregados As New HashSet(Of String)

        For x = 0 To ds2.Tables("dato").Rows.Count - 1
            cosa = ds2.Tables("dato").Rows(x)("Equipo").ToString.Trim
            cosita = ds2.Tables("dato").Rows(x)("Pais").ToString.Trim

            If Not paisesAgregados.Contains(cosita) Then
                paisesAgregados.Add(cosita)
                dPais.Items.Add(cosita)
            End If
            dCamisetas.Items.Add(cosa)
        Next
        dCamisetas.SelectedIndex = 0
        dPais.SelectedIndex = 0
        lCosaAgregar.Text = dCamisetas.SelectedItem.ToString
        lCositaAgregar.Text = dPais.SelectedItem.ToString
        lQueEs.Text = "Camiseta"
    End Sub

    '' filtrar camisetas por pais

    Sub FiltrarCamisetasPorPais(pais As String)
        Dim x As Integer = 0
        Dim cosa As String
        Dim query As String = "SELECT C.Equipo FROM web_camisetas C JOIN Paises P ON C.idPais = P.idPais WHERE P.Nombre = @pais ORDER BY C.Equipo"
        Dim cmd As New SqlCommand(query, con)
        cmd.Parameters.AddWithValue("@pais", pais)
        Dim da2 As New SqlDataAdapter(cmd)
        Dim ds2 As New DataSet

        dCamisetas.Items.Clear()
        da2.Fill(ds2, "dato")

        If ds2.Tables("dato").Rows.Count = 0 Then
            dCamisetas.Items.Add("*** No hay Camisetas disponibles en este momento... ***")
            Exit Sub
        End If

        For x = 0 To ds2.Tables("dato").Rows.Count - 1
            cosa = ds2.Tables("dato").Rows(x)("Equipo").ToString().Trim()
            dCamisetas.Items.Add(cosa)
        Next

        If dCamisetas.Items.Count > 0 Then
            dCamisetas.SelectedIndex = 0
            lCosaAgregar.Text = dCamisetas.SelectedItem.ToString()
        End If

        lQueEs.Text = "Camiseta"
    End Sub

    Sub CargaTemporal()

        Dim idU As Integer = VNum(Session("idUsuario"))
        Dim consulta As String = "Select item, cantidad, pais from web_pedidos_temporal where num_cli=" _
        & Session("idUsuario") & " order by item"
        Dim da1 As New SqlDataAdapter(consulta, con)

        Dim ds1 As New DataSet
        da1.Fill(ds1, "histo")
        gListaPedido.DataSource = ds1.Tables("histo")
        gListaPedido.DataBind()
        If ds1.Tables("histo").Rows.Count = 0 Then
            lErrorHistorico.Text = ""
            gListaPedido.Visible = False
            bEnvPedido.Visible = False
            bBorrarPedido.Visible = False
            Label7.Visible = False
        Else
            gListaPedido.Visible = True
            bEnvPedido.Visible = True
            bBorrarPedido.Visible = True
            Label7.Visible = True
        End If

    End Sub


    Protected Sub bInstrucciones_Click1(sender As Object, e As EventArgs)
        If bInstrucciones.Text = "Abrir Instrucciones" Then
            bInstrucciones.Text = "Cerrar Instrucciones"
            lInstrucciones.Visible = True
        Else
            bInstrucciones.Text = "Abrir Instrucciones"
            lInstrucciones.Visible = False
        End If
    End Sub

    Protected Sub dHelados_SelectedIndexChanged(sender As Object, e As EventArgs)
        lCosaAgregar.Text = dCamisetas.SelectedItem.ToString
        lQueEs.Text = "Camiseta"
    End Sub





    Protected Sub gListaPedido_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles gListaPedido.RowCommand

        Dim index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim row As GridViewRow = gListaPedido.Rows(index)
        Dim Item As String = row.Cells(1).Text, en As String = Chr(13) & Chr(10)
        Dim consulta As String = "delete web_pedidos_temporal where ltrim(rtrim(item))='" & Item & "' and num_cli=" & VNum(Session("idUsuario"))

        lErrorPedido.Text = ""
        If (e.CommandName = "Quitar") Then
            If SQL_Accion(consulta) = False Then
                lErrorPedido.Text = "No puedo quitar el item de la lista. Intente mas tade."
                Exit Sub
            End If
            CargaTemporal()
        End If
    End Sub


    Sub CargaHistorico()
        lErrorHistorico.Text = ""

        Dim query As String

        Select Case Session("QueEs")
            Case "Administradores"
                ' Si es administrador, cargar todos los pedidos
                query = "select npedido, fecha, estado from web_pedidos order by npedido desc"
            Case "Usuarios"
                Dim idU As Integer = VNum(Session("idUsuario"))
                ' Si es usuario, cargar solo sus pedidos
                query = "select npedido, fecha, estado from web_pedidos where num_cli=" & idU & " and estado<>'Enviado' order by npedido desc"
        End Select

        Dim da1 As New SqlDataAdapter(query, con)
        Dim ds1 As New DataSet
        da1.Fill(ds1, "histo")
        gHistorico.DataSource = ds1.Tables("histo")
        gHistorico.DataBind()


        If Session("QueEs") = "Administradores" Then
            gVerUnPedido.Columns(3).Visible = True
            gHistorico.Columns(5).Visible = True
        Else
            gVerUnPedido.Columns(3).Visible = False
            gHistorico.Columns(5).Visible = False
        End If

        If ds1.Tables("histo").Rows.Count = 0 Then
            lErrorHistorico.Text = "No hay pedidos anteriores o hubo un error al cargarlos. Reintente más tarde."
            gHistorico.Visible = False
        Else
            gHistorico.Visible = True
        End If

        pPedidos.Visible = False
        pHistorico.Visible = True
    End Sub




    Protected Sub gVerUnPedido_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs)
        Dim index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim row As GridViewRow = gVerUnPedido.Rows(index)
        Dim Item As String = row.Cells(0).Text
        Dim Npedido As Integer = Convert.ToInt32(Session("NPedido"))

        If (e.CommandName = "Quitar") Then

            lErrorVerUnPedido.Text = ""

            ' Primero, verifica si el ítem existe en la tabla del pedido
            Dim da1 As New SqlDataAdapter("SELECT * FROM web_pedidos_detalle WHERE NPedido=" & Npedido & " AND Item='" & Item & "'", con)
            Dim ds1 As New DataSet
            da1.Fill(ds1, "detalle")

            If ds1.Tables("detalle").Rows.Count = 0 Then
                lErrorVerUnPedido.Text = "El ítem no existe en el pedido. Reintente más tarde."
            Else
                ' Elimina el ítem del pedido
                If SQL_Accion("DELETE FROM web_pedidos_detalle WHERE NPedido=" & Npedido & " AND Item='" & Item & "'") = False Then
                    lErrorVerUnPedido.Text = "No he podido eliminar el ítem, hubo algún error de conexión. Por favor, reintente más tarde."
                Else
                    lErrorVerUnPedido.Text = "El ítem '" & Item & "' fue eliminado del pedido N° " & Npedido

                    ' Recargar la lista de ítems del pedido
                    Dim da2 As New SqlDataAdapter("SELECT web_pedidos_detalle.Item, web_pedidos_detalle.Cantidad, web_pedidos_detalle.Pais FROM web_pedidos_detalle WHERE web_pedidos_detalle.NPedido=" & Npedido & " ORDER BY web_pedidos_detalle.Item", con)
                    da2.Fill(ds1, "histo")
                    gVerUnPedido.DataSource = ds1.Tables("histo")
                    gVerUnPedido.DataBind()

                    If ds1.Tables("histo").Rows.Count = 0 Then
                        lErrorVerUnPedido.Text = "No hay más ítems en el pedido."
                        gVerUnPedido.Visible = False
                    Else
                        gVerUnPedido.Visible = True
                    End If
                End If
            End If
        End If
    End Sub




    Protected Sub gHistorico_RowCommand(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewCommandEventArgs)
        Dim index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim row As GridViewRow = gHistorico.Rows(index)
        Dim Npedido As Integer = VNum(row.Cells(2).Text), en As String = Chr(13) & Chr(10)
        Session("NPedido") = Npedido

        If (e.CommandName = "Ver") Then
            Label3.Text = "Detalle del Pedido N° " & Npedido

            lErrorVerUnPedido.Text = ""
            Dim da1 As New SqlDataAdapter("SELECT web_pedidos_detalle.Item, " &
                "web_pedidos_detalle.Cantidad, web_pedidos_detalle.Pais FROM web_pedidos_detalle INNER JOIN WEB_Camisetas ON " &
                "web_pedidos_detalle.Item = WEB_Camisetas.EQUIPO WHERE web_pedidos_detalle.NPedido=" &
                Npedido & " ORDER BY WEB_Camisetas.CODC", con)

            Dim ds1 As New DataSet
            da1.Fill(ds1, "histo")
            gVerUnPedido.DataSource = ds1.Tables("histo")
            gVerUnPedido.DataBind()
            If ds1.Tables("histo").Rows.Count = 0 Then
                lErrorHistorico.Text = "Hubo un error al cargar los items del pedido " & Npedido &
                ", porque no se leyó ninguno. Reintente más tarde."
                gVerUnPedido.Visible = False
                lErrorHistorico.Visible = True
            Else
                gVerUnPedido.Visible = True
            End If

            pHistorico.Visible = False
            pVerUnPedido.Visible = True
        End If


        If (e.CommandName = "Anular") Then
            'tengo que mirar primero el estado del pedido
            '(pudo haber dejado abierto con Solicitado pero ya se lo enviaron)
            lErrorHistorico.Text = ""
            Dim dal As New SqlDataAdapter("select estado from web_pedidos where npedido=" & Npedido, con)
            Dim ds1 As New DataSet
            dal.Fill(ds1, "histo")
            If ds1.Tables("histo").Rows.Count = 0 Then
                lErrorHistorico.Text = "No puedo acceder al pedido Nº " & Npedido & ". Reintente mas tarde."
                lErrorHistorico.Visible = True
                Exit Sub
            Else
                Dim Estado As String = ds1.Tables("histo")(0)("estado").ToString.Trim
                If Estado <> "Solicitado" Then
                    lErrorHistorico.Text = "El pedido tiene Estado='" & Estado & "' y ya no puede cancelarse por web (sólo 'Solicitado'). Llame a la fábrica " & "por favor."
                    lErrorHistorico.Visible = True
                    Exit Sub
                Else
                    If Estado = "Anulado" Then
                        lErrorHistorico.Text = "El pedido Nº " & Npedido & ", YA estaba Anulado ..."
                        lErrorHistorico.Visible = True
                        Exit Sub
                    End If
                    lErrorHistorico.Text = ""
                    If SQL_Accion("update web_pedidos set estado='Anulado' where npedido=" & Npedido) = False Then
                        lErrorHistorico.Text = "No he podido cambiar el estado, hubo algún error de conexión. Por favor, reintente mas tarde o llame a la fabrica para avisar de la anulación."
                        lErrorHistorico.Visible = True
                        Exit Sub
                    Else
                        lPedidoAnulado.Text = "El pedido Nº " & Npedido & ", fue ANULADO "
                        pHistorico.Visible = False
                        pPedidoAnulado.Visible = True
                        lErrorHistorico.Visible = False
                        Exit Sub
                    End If
                End If
            End If
        End If



        If (e.CommandName = "Desp") Then
            ' Despachar el pedido
            lErrorHistorico.Text = ""
            Dim dal As New SqlDataAdapter("select estado from web_pedidos where npedido=" & Npedido, con)
            Dim ds1 As New DataSet
            dal.Fill(ds1, "histo")
            If ds1.Tables("histo").Rows.Count = 0 Then
                lErrorHistorico.Text = "No puedo acceder al pedido Nº " & Npedido & ". Reintente más tarde."
                lErrorHistorico.Visible = True
                Exit Sub
            Else
                Dim Estado As String = ds1.Tables("histo")(0)("estado").ToString.Trim
                If Estado <> "Solicitado" Then
                    lErrorHistorico.Text = "El pedido tiene Estado='" & Estado & "' y ya no puede ser despachado (sólo 'Solicitado')."
                    lErrorHistorico.Visible = True
                    Exit Sub
                Else
                    If SQL_Accion("update web_pedidos set estado='Despachado' where npedido=" & Npedido) = False Then
                        lErrorHistorico.Text = "No he podido cambiar el estado a 'Despachado', hubo algún error de conexión. Por favor, reintente más tarde."
                        lErrorHistorico.Visible = True
                        Exit Sub
                    Else
                        lPedidoAnulado.Text = "El pedido Nº " & Npedido & ", fue DESPACHADO"
                        pHistorico.Visible = False
                        pPedidoAnulado.Visible = True
                        lErrorHistorico.Visible = False
                        Exit Sub
                    End If
                End If
            End If
        End If

    End Sub



    Protected Sub dPais_SelectedIndexChanged(sender As Object, e As EventArgs)
        FiltrarCamisetasPorPais(dPais.SelectedItem.ToString())
        lCositaAgregar.Text = dPais.SelectedItem.ToString
        dCamisetas.Visible = True
    End Sub



    Function EnviarMail(ByVal EmailDestino As String, ByVal Subject As String, ByVal Mensaje As String) As String
        Dim Resultado As String = "OK"
        Dim SmtpServer As New SmtpClient()
        Dim mail As New MailMessage()

        Try
            mail = New MailMessage()

            mail.From = New MailAddress(Email, "CamisetasNani")
            mail.To.Add(EmailDestino)
            mail.Subject = Subject
            mail.Body = Mensaje
            mail.IsBodyHtml = False
            mail.Priority = MailPriority.Normal

            If EmailActivo = "EmailGmail" Then
                SmtpServer.Credentials = New Net.NetworkCredential(Email, EmailPass)
                SmtpServer.Host = "aspmx.l.google.com"
                SmtpServer.Port = 587
                SmtpServer.EnableSsl = True
            End If

            SmtpServer.Send(mail)
        Catch
            Resultado = Err.Description
        End Try
        mail.Dispose()
        Return Resultado
    End Function







    Protected Sub bCambioProducto_Click(sender As Object, e As EventArgs)
        pAreaUsuario.Visible = False
        pAbmCamisetas.Visible = True
        CargarCamisetas()
        CargarPaises()
    End Sub
















    '' Pruebas abm admin



    Private Sub CargarAdministradores()
        ' Cargar la lista de administradores desde la base de datos
        Dim query As String = "SELECT * FROM Administradores WHERE activo = 1"
        Dim ds As New DataSet()
        Using da As New SqlDataAdapter(query, con)
            da.Fill(ds)
            gAdministradores.DataSource = ds
            gAdministradores.DataBind()
        End Using

    End Sub

    Protected Sub bGuardar_Click(sender As Object, e As EventArgs)
        Dim id As Integer = If(ViewState("IdAdmin") IsNot Nothing, Convert.ToInt32(ViewState("IdAdmin")), 0)
        Dim nombre As String = tNombre.Text.Trim()
        Dim apellido As String = tApellido.Text.Trim()
        Dim usuario As String = tUsuarioA.Text.Trim()
        Dim pass As String = tClaveA.Text.Trim()
        Dim email As String = tEmail.Text.Trim()

        If nombre.Length < 3 Or nombre.Length > 50 Then
            lErrorAdministradores.Text = "El nombre debe tener entre 3 y 50 caracteres."
            lErrorAdministradores.Visible = True
            Exit Sub
        End If

        If apellido.Length < 3 Or apellido.Length > 50 Then
            lErrorAdministradores.Text = "El apellido debe tener entre 3 y 50 caracteres."
            lErrorAdministradores.Visible = True
            Exit Sub
        End If

        If usuario.Length < 5 Or usuario.Length > 20 Then
            lErrorAdministradores.Text = "El usuario debe tener entre 5 y 20 caracteres."
            lErrorAdministradores.Visible = True
            Exit Sub
        End If

        If pass.Length < 5 Or pass.Length > 10 Then
            lErrorAdministradores.Text = "La contraseña debe tener entre 5 y 10 caracteres."
            lErrorAdministradores.Visible = True
            Exit Sub
        End If

        If Not email.Contains("@") Or Not email.Contains(".") Then
            lErrorAdministradores.Text = "El email no es válido."
            lErrorAdministradores.Visible = True
            Exit Sub
        End If

        Dim queryUsuarioExistente As String = "SELECT COUNT(*) FROM Administradores WHERE Usuario = @Usuario AND IdAdmin <> @IdAdmin "
        Using cmd As New SqlCommand(queryUsuarioExistente, con)
            cmd.Parameters.AddWithValue("@Usuario", usuario)
            cmd.Parameters.AddWithValue("@IdAdmin", id)
            con.Open()
            Dim count As Integer = Convert.ToInt32(cmd.ExecuteScalar())
            con.Close()
            If count > 0 Then
                lErrorAdministradores.Text = "El usuario ya existe. Por favor, elija otro nombre de usuario."
                lErrorAdministradores.Visible = True
                Exit Sub
            End If
        End Using

        If id = 0 Then
            ' Insertar nuevo administrador

            If SQL_Accion("INSERT INTO Administradores (Nombre, Apellido, Usuario, Pass, Email, activo) VALUES ('" _
                      & nombre & "','" & apellido & "','" & usuario & "','" & pass & "','" & email & "', 1)") Then
                lErrorAdministradores.Text = "Administrador agregado exitosamente."
            Else
                lErrorAdministradores.Text = "Error al agregar administrador."
            End If
        Else
            ' Actualizar administrador existente
            If SQL_Accion("UPDATE Administradores SET Nombre = '" & nombre & "', Apellido = '" & apellido & "', Usuario = '" & usuario & "', Pass = '" & pass & "', Email = '" & email & "' WHERE IdAdmin = " & id) Then
                lErrorAdministradores.Text = "Administrador actualizado exitosamente."
            Else
                lErrorAdministradores.Text = "Error al actualizar administrador."
            End If
        End If

        lErrorAdministradores.Visible = True
        CargarAdministradores()
        LimpiarFormulario()
        pAgregarEditar.Visible = False
        pLista.Visible = True
    End Sub

    Protected Sub gAdministradores_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs)
        Dim index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim row As GridViewRow = gAdministradores.Rows(index)
        Dim idAdmin As Integer = Convert.ToInt32(row.Cells(0).Text)

        If e.CommandName = "Editar" Then
            ' Cargar datos del administrador en el formulario
            Dim query As String = "SELECT * FROM Administradores WHERE IdAdmin = @Id"
            Dim ds As New DataSet()

            Using da As New SqlDataAdapter(query, con)
                da.SelectCommand.Parameters.AddWithValue("@Id", idAdmin)
                da.Fill(ds)
                If ds.Tables(0).Rows.Count > 0 Then
                    tNombre.Text = ds.Tables(0).Rows(0)("Nombre").ToString()
                    tApellido.Text = ds.Tables(0).Rows(0)("Apellido").ToString()
                    tUsuarioA.Text = ds.Tables(0).Rows(0)("Usuario").ToString()
                    tClaveA.Text = ds.Tables(0).Rows(0)("Pass").ToString()
                    tEmail.Text = ds.Tables(0).Rows(0)("Email").ToString()
                    ViewState("IdAdmin") = idAdmin
                    pAgregarEditar.Visible = True
                    pLista.Visible = False
                End If
            End Using

        ElseIf e.CommandName = "Eliminar" Then
            ' Eliminar administrador de la base de datos

            If SQL_Accion("UPDATE Administradores SET activo = 0 WHERE IdAdmin = " & idAdmin) Then
                lErrorAdministradores.Text = "Administrador marcado como inactivo exitosamente."
            Else
                lErrorAdministradores.Text = "Error al marcar administrador como inactivo."
            End If

            lErrorAdministradores.Visible = True
            CargarAdministradores()
        End If
    End Sub

    Private Sub LimpiarFormulario()
        tNombre.Text = String.Empty
        tApellido.Text = String.Empty
        tUsuarioA.Text = String.Empty
        tClaveA.Text = String.Empty
        tEmail.Text = String.Empty
        ViewState("IdAdmin") = Nothing
    End Sub

    Protected Sub bCancelVolverABMAdmin_Click(sender As Object, e As EventArgs)
        pAbmAdmin.Visible = False
        pAreaUsuario.Visible = True
    End Sub

    Protected Sub bABMAdmin_Click(sender As Object, e As EventArgs)
        pAreaUsuario.Visible = False
        pAbmAdmin.Visible = True
        CargarAdministradores()
    End Sub

    Protected Sub bCancelar_Click(sender As Object, e As EventArgs)
        LimpiarFormulario()
        pAgregarEditar.Visible = False
        pLista.Visible = True
    End Sub

    Protected Sub bNuevo_Click(sender As Object, e As EventArgs)
        LimpiarFormulario()
        pAgregarEditar.Visible = True
        pLista.Visible = False
    End Sub

















    ''abm camisetas

    Private Sub CargarPaises()
        Dim query As String = "SELECT idpais, nombre FROM paises"
        Dim ds As New DataSet()
        Using da As New SqlDataAdapter(query, con)
            da.Fill(ds)
            ddlPaises.DataSource = ds
            ddlPaises.DataTextField = "nombre"
            ddlPaises.DataValueField = "idpais"
            ddlPaises.DataBind()
        End Using
    End Sub

    Private Sub CargarCamisetas()
        ' Cargar la lista de camisetas desde la base de datos
        Dim query As String = "SELECT c.codc, c.equipo, p.nombre AS Pais FROM web_camisetas c INNER JOIN paises p ON c.idpais = p.idpais"
        Dim ds As New DataSet()
        Using da As New SqlDataAdapter(query, con)
            da.Fill(ds)
            gCamisetas.DataSource = ds
            gCamisetas.DataBind()
        End Using
    End Sub

    Protected Sub bGuardarAbmCami_Click(sender As Object, e As EventArgs)
        Dim codc As Integer = If(ViewState("CodCamiseta") IsNot Nothing, Convert.ToInt32(ViewState("CodCamiseta")), 0)
        Dim equipo As String = tEquipo.Text.Trim()
        Dim idpais As Integer = Convert.ToInt32(ddlPaises.SelectedValue)

        ' Validaciones
        If equipo.Length < 3 Or equipo.Length > 50 Then
            lErrorCamisetas.Text = "El nombre del equipo debe tener entre 3 y 50 caracteres."
            lErrorCamisetas.Visible = True
            Exit Sub
        End If

        ' Verificar si el equipo ya existe
        Dim queryCheck As String = "SELECT COUNT(*) FROM web_camisetas WHERE equipo = @equipo AND codc <> @codc"
        Dim cmdCheck As New SqlCommand(queryCheck, con)
        cmdCheck.Parameters.AddWithValue("@equipo", equipo)
        cmdCheck.Parameters.AddWithValue("@codc", codc)
        con.Open()
        Dim exists As Integer = Convert.ToInt32(cmdCheck.ExecuteScalar())
        con.Close()
        If exists > 0 Then
            lErrorCamisetas.Text = "El equipo ya existe."
            lErrorCamisetas.Visible = True
            Exit Sub
        End If

        If codc = 0 Then
            ' Insertar nueva camiseta
            If SQL_Accion("INSERT INTO web_camisetas (equipo, idpais) VALUES ('" & equipo & "'," & idpais & ")") Then
                lErrorCamisetas.Text = "Camiseta agregada exitosamente."
            Else
                lErrorCamisetas.Text = "Error al agregar camiseta."
            End If
        Else
            ' Actualizar camiseta existente
            If SQL_Accion("UPDATE web_camisetas SET equipo = '" & equipo & "', idpais = " & idpais & " WHERE codc = " & codc) Then
                lErrorCamisetas.Text = "Camiseta actualizada exitosamente."
            Else
                lErrorCamisetas.Text = "Error al actualizar camiseta."
            End If
        End If

        lErrorCamisetas.Visible = True
        CargarCamisetas()
        LimpiarFormularioCam()
        pAgregarEditarCami.Visible = False
        pListaCami.Visible = True
    End Sub

    Protected Sub gCamisetas_RowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs)
        Dim index As Integer = Convert.ToInt32(e.CommandArgument)
        Dim row As GridViewRow = gCamisetas.Rows(index)
        Dim codc As Integer = Convert.ToInt32(row.Cells(0).Text)

        If e.CommandName = "Editar" Then
            ' Cargar datos de la camiseta en el formulario
            Dim query As String = "SELECT * FROM web_camisetas WHERE codc = @CodCamiseta"
            Dim ds As New DataSet()

            Using da As New SqlDataAdapter(query, con)
                da.SelectCommand.Parameters.AddWithValue("@CodCamiseta", codc)
                da.Fill(ds)
                If ds.Tables(0).Rows.Count > 0 Then
                    tEquipo.Text = ds.Tables(0).Rows(0)("equipo").ToString()
                    ddlPaises.SelectedValue = ds.Tables(0).Rows(0)("idpais").ToString()
                    ViewState("CodCamiseta") = codc
                    pAgregarEditarCami.Visible = True
                    pListaCami.Visible = False
                End If
            End Using

        ElseIf e.CommandName = "Eliminar" Then
            ' Eliminar camiseta de la base de datos
            If SQL_Accion("DELETE FROM web_camisetas WHERE codc = " & codc) Then
                lErrorCamisetas.Text = "Camiseta eliminada exitosamente."
            Else
                lErrorCamisetas.Text = "Error al eliminar camiseta."
            End If

            lErrorCamisetas.Visible = True
            CargarCamisetas()
        End If
    End Sub

    Private Sub LimpiarFormularioCam()
        tEquipo.Text = String.Empty
        ddlPaises.SelectedIndex = 0
        lErrorCamisetas.Visible = False
    End Sub

    Protected Sub bAgregar_Click(sender As Object, e As EventArgs)
        LimpiarFormularioCam()
        ViewState("CodCamiseta") = Nothing
        pAgregarEditarCami.Visible = True
        pListaCami.Visible = False
    End Sub

    Protected Sub bCancelarVolverABMCami_Click(sender As Object, e As EventArgs)

        LimpiarFormularioCam()
        pAbmCamisetas.Visible = False
        pAreaUsuario.Visible = True

    End Sub

    Protected Sub bCancelarCami_Click(sender As Object, e As EventArgs)
        pAgregarEditarCami.Visible = False
        pListaCami.Visible = True
        LimpiarFormularioCam()
    End Sub

    Protected Sub bIngresoLogo_Click(sender As Object, e As EventArgs)
        pPortada.Visible = False
        pLoginMenu.Visible = True

    End Sub

    Protected Sub canVolUsuAdm_Click(sender As Object, e As EventArgs)
        pAdminUsuLog.Visible = False
        pLoginMenu.Visible = True
    End Sub

    Protected Sub bCanVolLoginMenu_Click(sender As Object, e As EventArgs)
        pLoginMenu.Visible = False
        pPortada.Visible = True
    End Sub

    Protected Sub bLogLoginMenu_Click(sender As Object, e As EventArgs)
        pLoginMenu.Visible = False
        pAdminUsuLog.Visible = True
    End Sub

    Protected Sub bRegisLoginMenu_Click(sender As Object, e As EventArgs)
        pLoginMenu.Visible = False
        pRegistrarse.Visible = True
    End Sub

    Protected Sub bModDatos_Click(sender As Object, e As EventArgs)
        pAreaUsuario.Visible = False
        pCambiarDatosPersonalesU.Visible = True
    End Sub

    Protected Sub bPedidosAFab_Click(sender As Object, e As EventArgs)
        pAreaUsuario.Visible = False
        pPedidos.Visible = True
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs)
        LimpiarLogin()
        LimpiarRegistroU()
        lReenviarClave.Text = ""
        pLogin.Visible = True
        pAreaUsuario.Visible = False
        tUsuario.Text = Session("Usuario")
        tClave.Text = Session("Password")
    End Sub

    Protected Sub bRecClave_Click(sender As Object, e As EventArgs)
        Dim usu As String = tUsuario.Text.Trim.ToUpper, email As String, xusuario As String, mensaje As String, pass As String
        Dim en As String = Chr(13) & Chr(10)
        If comprobar(usu) = False Then
            lReenviarClave.Text = "*** El usuario es incorrecto. Revisa por favor. ***"
            lReenviarClave.Visible = True
            Exit Sub
        End If

        Dim da2 As New SqlDataAdapter("select ltrim(rtrim(nombre)) + ' ' + ltrim(rtrim(apellido)) as usuario,pass," &
        " email from usuarios where upper(ltrim(rtrim(usuario)))='" & usu & "'", con)
        Dim ds2 As New DataSet
        da2.Fill(ds2, "Login")
        If ds2.Tables("Login").Rows.Count = 0 Then
            lReenviarClave.Text = "*** El usuario es incorrecto. Revisa por favor. ***"
            lReenviarClave.Visible = True
            Exit Sub
        End If

        email = ds2.Tables("Login").Rows(0)("email").ToString.Trim.ToLower
        pass = ds2.Tables("Login").Rows(0)("pass").ToString.Trim.ToLower
        xusuario = ds2.Tables("Login").Rows(0)("usuario").ToString.Trim


        mensaje = "Hola" & xusuario & "." & en & en & "Le escribimos desde camisetasNani, respondiendo a su pedido" &
        "de recuperacion de clave." & en & en & "Su usuario es " & """" & usu & """" & en & "Su clave es" &
        """" & pass & """" & en & en & "Ya podes volver a la plataforma!"

        Dim ok As String = EnviarMail(email, "Camisetas Nani - Clave Recuperada", mensaje)
        If ok = "OK" Then
            lReenviarClave.Text = "*** Ya te enviamos un mail con la clave! ***"
        Else
            lReenviarClave.Text = "*** Hubo un error al querer enviar el mail.. ***" + ok
        End If
        lReenviarClave.Visible = True


    End Sub

    Protected Sub bVologUsuAdm_Click(sender As Object, e As EventArgs)
        pAdminUsuLog.Visible = True
        pLogin.Visible = False
        lErrorLogin.Visible = False
        lReenviarClave.Visible = False
    End Sub

    Protected Sub bAhoraQHago_Click(sender As Object, e As EventArgs)
        pAreaUsuario.Visible = False
        pAhoraQueHago.Visible = True
    End Sub

    Protected Sub bVolverLogPR_Click(sender As Object, e As EventArgs)
        pAhoraQueHago.Visible = False
        pAreaUsuario.Visible = True
    End Sub

    Protected Sub canVolCam_Click(sender As Object, e As EventArgs)
        LimpiarRegistroEditU()
        pCambiarDatosPersonalesU.Visible = False
        pAreaUsuario.Visible = True
    End Sub

    Protected Sub bCambDatos_Click(sender As Object, e As EventArgs)
        Dim ok As Boolean = True

        ' Validar los campos que el usuario puede cambiar
        LimpiarErroresEditU()
        ArreglarCampo(ctEmailU)
        If Not ValidateEmail(ctEmailU.Text) Then
            ok = False
            clEEmailU.Text = "El correo electrónico no es válido."
            clEEmailU.Visible = True
        End If

        ctLocalidadU.Text = ctLocalidadU.Text.Trim().ToUpper
        If Not comprobar(ctLocalidadU.Text) Then
            ArreglarCampo(ctLocalidadU)
            ok = False
            clELocalidadU.Text = "La Localidad contenía caracteres inválidos, fueron quitados."
            clELocalidadU.Visible = True
        End If

        ctDireccionU.Text = ctDireccionU.Text.Trim().ToUpper
        If Not comprobar(ctDireccionU.Text) Then
            ArreglarCampo(ctDireccionU)
            ok = False
            clEDireccionU.Text = "La Dirección contenía caracteres inválidos, fueron quitados."
            clEDireccionU.Visible = True
        End If

        ctTelefonosU.Text = ctTelefonosU.Text.Trim().ToUpper
        If Not comprobar(ctTelefonosU.Text) Then
            ArreglarCampo(ctTelefonosU)
            ok = False
            clETelefonosU.Text = "El teléfono contenía caracteres inválidos, fueron quitados."
            clETelefonosU.Visible = True
        End If

        ctUsuarioU.Text = ctUsuarioU.Text.Trim().ToUpper()
        If Not comprobar(ctUsuarioU.Text) OrElse ctUsuarioU.Text.IndexOf(" ") > -1 Then
            ctUsuarioU.Text = ctUsuarioU.Text.Replace(" ", "")
            ArreglarCampo(ctUsuarioU)
            ok = False
            clEUsuarioU.Text = "El usuario contenía caracteres inválidos, fueron quitados."
            clEUsuarioU.Visible = True
        End If

        If ctUsuarioU.Text.Length < 5 Then
            ok = False
            clEUsuarioU.Text = "El usuario debe tener entre 5 y 10 caracteres, letras y/o números."
            clEUsuarioU.Visible = True
        End If

        ctPassU.Text = ctPassU.Text.Trim()
        If Not comprobar(ctPassU.Text) OrElse tPassU.Text.IndexOf(" ") > -1 Then
            ctPassU.Text = ctPassU.Text.Replace(" ", "")
            ArreglarCampo(ctPassU)
            ok = False
            clEPassU.Text = "La clave contenía caracteres inválidos. Pruebe con letras y números."
            clEPassU.Visible = True
        End If

        If ctPassU.Text.Length < 5 Then
            ok = False
            clEPassU.Text = "La clave debe tener entre 5 y 10 caracteres, letras y/o números."
            clEPassU.Visible = True
        End If

        ctDocU.Text = ctDocU.Text.Trim
        If comprobar(ctDocU.Text) = False Or Not IsNumeric(ctDocU.Text) Then
            SoloNumeros(ctDocU)
            ok = False
            clEDocU.Text = "El documento no era valido, se ajusto a numero."
            clEDocU.Visible = True
        End If

        If ctDocU.Text.Length < 8 Then
            ok = False
            clEDocU.Text = "El documento tiene minimo 8 Caracteres."
            clEDocU.Visible = True
        End If


        Session("Usuario") = ctUsuarioU.Text
        Session("Documento") = ctDocU.Text.Trim

        Select Case Session("QueEs")
            Case "Usuarios"
                If YaExisteSQL("SELECT idusuario FROM usuarios WHERE usuario='" & Session("Usuario") & "' AND idusuario <> " & Session("idUsuario")) Then
                    ok = False
                    clEUsuarioU.Text = "El usuario elegido ya existe. Pruebe con otro."
                    clEUsuarioU.Visible = True
                End If

                If YaExisteSQL("SELECT idusuario FROM usuarios WHERE doc='" & Session("Documento") & "' AND idusuario <> " & Session("idUsuario")) Then
                    ok = False
                    clEDocU.Text = "El documento elegido ya existe. Pruebe con otro."
                    clEDocU.Visible = True
                End If

            Case "Administradores"
                If YaExisteSQL("SELECT idadmin FROM administradores WHERE usuario='" & Session("Usuario") & "' AND idadmin <> " & Session("idAdmin")) Then
                    ok = False
                    clEUsuarioU.Text = "El usuario elegido ya existe. Pruebe con otro."
                    clEUsuarioU.Visible = True
                End If

                If YaExisteSQL("SELECT idadmin FROM administradores WHERE doc='" & Session("Documento") & "' AND idadmin <> " & Session("idAdmin")) Then
                    ok = False
                    clEDocU.Text = "El documento elegido ya existe. Pruebe con otro."
                    clEDocU.Visible = True
                End If
        End Select






        If ok Then
            Select Case Session("QueEs")
                Case "Usuarios"
                    SQL_Accion("UPDATE usuarios SET Email = '" & ctEmailU.Text.Trim() & "', IdProv = '" & cdProvinciaU.SelectedValue & "', Localidad = '" & ctLocalidadU.Text.Trim() & "', Doc = '" & ctDocU.Text.Trim() & "', Direccion = '" & ctDireccionU.Text.Trim() & "', Telefonos = '" & ctTelefonosU.Text.Trim() & "', Usuario = '" & ctUsuarioU.Text.Trim() & "', Pass = '" & ctPassU.Text.Trim() & "' WHERE idUsuario = " & Session("idUsuario"))
                Case "Administradores"
                    SQL_Accion("UPDATE administradores SET Email = '" & ctEmailU.Text.Trim() & "', IdProv = '" & cdProvinciaU.SelectedValue & "', Localidad = '" & ctLocalidadU.Text.Trim() & "', Doc = '" & ctDocU.Text.Trim() & "', Direccion = '" & ctDireccionU.Text.Trim() & "', Telefonos = '" & ctTelefonosU.Text.Trim() & "', Usuario = '" & ctUsuarioU.Text.Trim() & "', Pass = '" & ctPassU.Text.Trim() & "' WHERE idAdmin = " & Session("idAdmin"))


            End Select
            'Apellido, Nombre, tDoc, doc, Usuario, Pass, Email, IdProv, Localidad, Direccion, Telefonos, fNacimiento

            LimpiarRegistroEditU()
            pCambiarDatosPersonalesU.Visible = False
            pCambioExitoso.Visible = True
        Else
            ' Mostrar mensaje de error
            cError.Text = "Revise los errores por favor y luego reintente."
            cError.Visible = True
        End If
    End Sub

    Protected Sub bVolverUsu_Click(sender As Object, e As EventArgs)
        pCambioExitoso.Visible = False
        pAreaUsuario.Visible = True
    End Sub

    Protected Sub bCanVolPed_Click(sender As Object, e As EventArgs)
        pPedidos.Visible = False
        pAreaUsuario.Visible = True
    End Sub

    Protected Sub bNuevoPedido_Click(sender As Object, e As EventArgs)
        Dim en As String = "</BR>"
        lInstrucciones.Text = "INSTRUCCIONES:" & en & en & "1- Seleccionar Pais de La Camiseta a Elegir" & en &
            "2- Elegir Equipo y Cantidad Luego Tocar Agregar Para Sumarlo Al Pedido" & en &
            "3- En Caso De Querer Quitar Un Pedido, Tocar En Quitar Ubicado En La Grilla  " & en &
            "4- En Caso De Querer Quitar Todos Los Pedidos, Tocar En Quitar Todos" & en &
            "5- Al Terminar Pulsar El Boton Enviar Pedido Para Mandar El Pedido."
        lInstrucciones.Visible = False
        bInstrucciones.Text = "Abrir Instrucciones"

        pPedidos.Visible = False
        lCosaAgregar.Text = ""
        lCositaAgregar.Text = ""
        dCamisetas.Visible = False
        CargarHelados()
        ''cosita nueva
        AddHandler dPais.SelectedIndexChanged, AddressOf dPais_SelectedIndexChanged


        bBorrarPedido.Visible = False
        bEnvPedido.Visible = False
        lErrorPedido.Text = ""
        'BORRO EL TEMPORAL DE PEDIDOS PARA EL CLIENTE
        If IsNothing(Session("idUsuario")) Then
            pLogin.Visible = True
            Exit Sub
        End If
        SQL_Accion("delete web_pedidos_temporal where num_cli=" & Session("idUsuario"))
        CargaTemporal()
        pNuevoPedido.Visible = True
    End Sub

    Protected Sub bTodosLosPedidos_Click(sender As Object, e As EventArgs)
        CargaHistorico()
    End Sub

    Protected Sub bTermVolverPed_Click(sender As Object, e As EventArgs)
        pPedidoCreado.Visible = False
        pPedidos.Visible = True
    End Sub

    Protected Sub bTermVolverAnu_Click(sender As Object, e As EventArgs)
        pPedidoAnulado.Visible = False
        pPedidos.Visible = True
    End Sub

    Protected Sub bTermHisto_Click(sender As Object, e As EventArgs)
        pHistorico.Visible = False
        pPedidos.Visible = True
        lErrorHistorico.Visible = False
    End Sub

    Protected Sub bActHisto_Click(sender As Object, e As EventArgs)
        CargaHistorico()
        lErrorHistorico.Visible = False
    End Sub

    Protected Sub bVolverDetalle_Click(sender As Object, e As EventArgs)
        pVerUnPedido.Visible = False
        pHistorico.Visible = True
        lErrorVerUnPedido.Visible = False
    End Sub

    Protected Sub bAgregarLista_Click(sender As Object, e As EventArgs)
        Dim x As Integer = 0, c As String, numero As Integer = 0
        Dim Cosa As String = lCosaAgregar.Text.Trim
        Dim Cosita As String = lCositaAgregar.Text.Trim
        If Cosa = "----------" Then Exit Sub

        Dim Cantidad As Integer = VNum(tCantLatas.SelectedValue.ToString)

        If Cantidad <= 0 Then Exit Sub
        lErrorPedido.Text = ""

        'Veo si ya esta el item en la lista
        Dim da2 As New SqlDataAdapter("Select cantidad from web_pedidos_temporal where num_cli=" _
            & Session("idUsuario") & " And LTrim(RTrim(item)) ='" & Cosa & "'", con)
        Dim ds2 As New DataSet
        da2.Fill(ds2, "dato")
        If ds2.Tables("dato").Rows.Count > 0 Then

            Cantidad += VNum(ds2.Tables("dato").Rows(0)("cantidad"))
            If SQL_Accion("update web_pedidos_temporal set cantidad=" & Cantidad & " where num_cli=" _
                & Session("idUsuario") & " and ltrim(rtrim(item))='" & Cosa & "'") = False Then
                lErrorPedido.Text = "No puedo modificar el item elegido. Intente mas tarde."
                Exit Sub
            End If

        Else

            If SQL_Accion("Insert into web_pedidos_temporal (num_cli, item, pais, cantidad) values (" &
                Session("idUsuario") & ", '" & Cosa & "',  '" & Cosita & "', " & Cantidad & ")") = False Then
                lErrorPedido.Text = "No puedo agregar el item a la lista. Intente mas tarde."
                Exit Sub
            End If
        End If
        tCantLatas.SelectedIndex = 0
        CargaTemporal()
    End Sub

    Protected Sub bEnvPedido_Click(sender As Object, e As EventArgs)
        Dim idU As Integer = VNum(Session("idUsuario"))
        Dim npedido As Integer = 0, vItem As String = "", vTipo As String = "",
        vCantidad As Integer = 0, cosa As String = ""
        Dim linea As String = "", em As String = Chr(13) & Chr(10)
        lErrorPedido.Text = ""
        If SQL_Accion("insert into web_pedidos (num_cli) values (" _
        & Session("idUsuario") & ")") = True Then

            Dim da2 As New SqlDataAdapter("select top 1 npedido from web_pedidos where num_cli=" _
            & Session("idUsuario") & " order by npedido desc", con)
            Dim ds2 As New DataSet
            da2.Fill(ds2, "dato")
            If ds2.Tables("dato").Rows.Count > 0 Then
                npedido = ds2.Tables("dato").Rows(0)("npedido")
                ' obtengo el número que le asignó y lo uso para crear los detalles
                If SQL_Accion("INSERT INTO WEB_Pedidos_detalle (item, Cantidad, Pais, nPedido) " &
                "SELECT WEB_Pedidos_Temporal.Item, WEB_Pedidos_Temporal.Cantidad, WEB_Pedidos_Temporal.Pais," &
                npedido & " As nPedido FROM WEB_Pedidos_Temporal where num_cli=" & idU) = True Then
                    lPedidoCreado.Text = "El pedido N° " & npedido & ", fue creado correctamente."
                    pNuevoPedido.Visible = False
                    pPedidoCreado.Visible = True
                    If SQL_Accion("delete web_pedidos_temporal where num_cli=" & idU) = False Then
                    End If
                Else
                    lErrorPedido.Text = "Hubo un error al intentar guardar el detalle del pedido " & npedido & ", que quedó vacío o con carga parcial. Anúllelo e Intente más tarde"
                    Exit Sub
                End If
            Else
                lErrorPedido.Text = "Hubo un error al intentar guardar el detalle del pedido " & npedido & ", que quedó vacío. Anúllelo e Intente más tarde"
                Exit Sub
            End If
        Else
            lErrorPedido.Text = "Hubo un error al intentar guardar el pedido. Intente más tarde"
        End If
    End Sub

    Protected Sub bCanPedido_Click(sender As Object, e As EventArgs)
        If SQL_Accion("delete web_pedidos_temporal where num_cli=" & VNum(Session("idUsuario"))) = False Then
        End If
        pNuevoPedido.Visible = False
        pPedidos.Visible = True
    End Sub

    Protected Sub bEliminarUsuario_Click(sender As Object, e As EventArgs)
        Dim usuario As String = tUsername.Text.Trim()
        Dim razon As String = tRazonEliminacion.Text.Trim()

        If String.IsNullOrEmpty(usuario) Then
            lResultadoAccion.Text = "Por favor, ingrese un nombre de usuario."
            lResultadoAccion.Visible = True
            Return
        End If

        If String.IsNullOrEmpty(razon) Then
            lResultadoAccion.Text = "Por favor, ingrese una razón para la eliminación."
            lResultadoAccion.Visible = True
            Return
        End If

        Dim query As String = "DELETE FROM Usuarios WHERE Usuario = @Usuario"

        Using cmd As New SqlCommand(query, con)
            cmd.Parameters.AddWithValue("@Usuario", usuario)
            con.Open()
            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()
            con.Close()

            If rowsAffected > 0 Then
                lResultadoAccion.Text = "Usuario eliminado exitosamente."
                lResultadoAccion.ForeColor = System.Drawing.Color.Green
            Else
                lResultadoAccion.Text = "No se encontró el usuario o no se pudo eliminar."
                lResultadoAccion.ForeColor = System.Drawing.Color.Red
            End If
            lResultadoAccion.Visible = True
        End Using

    End Sub

    Protected Sub bDesactivarUsuario_Click(sender As Object, e As EventArgs)
        Dim usuario As String = tUsername.Text.Trim()
        Dim razon As String = tRazonEliminacion.Text.Trim()

        If String.IsNullOrEmpty(usuario) Then
            lResultadoAccion.Text = "Por favor, ingrese un nombre de usuario."
            lResultadoAccion.Visible = True
            Return
        End If


        If String.IsNullOrEmpty(razon) Then
            lResultadoAccion.Text = "Por favor, ingrese una razón para la eliminación."
            lResultadoAccion.Visible = True
            Return
        End If


        Dim query As String = "UPDATE Usuarios SET Activo = 0 WHERE Usuario = @Usuario"

        Using cmd As New SqlCommand(query, con)
            cmd.Parameters.AddWithValue("@Usuario", usuario)
            con.Open()
            Dim rowsAffected As Integer = cmd.ExecuteNonQuery()
            con.Close()

            If rowsAffected > 0 Then
                lResultadoAccion.Text = "Usuario desactivado exitosamente."
                lResultadoAccion.ForeColor = System.Drawing.Color.Green
            Else
                lResultadoAccion.Text = "No se encontró el usuario o no se pudo desactivar."
                lResultadoAccion.ForeColor = System.Drawing.Color.Red
            End If
            lResultadoAccion.Visible = True
        End Using

    End Sub

    Protected Sub bDesEliUsu_Click(sender As Object, e As EventArgs)
        pUserActions.Visible = True
        pAreaUsuario.Visible = False
    End Sub

    Protected Sub bCanVolverDesUsu_Click(sender As Object, e As EventArgs)
        pUserActions.Visible = False
        pAreaUsuario.Visible = True
    End Sub

    Protected Sub bBorrarPedido_Click(sender As Object, e As EventArgs)
        If SQL_Accion("delete web_pedidos_temporal where num_cli=" & VNum(Session("idUsuario"))) = False Then
            lErrorPedido.Text = "No puedo quitar a todos los items de la lista. Intente mas tarde."
            Exit Sub
        End If
        CargaTemporal()
    End Sub


    Public Function CreaCodigo(ByVal cantCaracteres As Integer) As String
        Dim strRand As String = Nothing, r As New Random, c As String, i As Integer


        For i = 0 To cantCaracteres - 1
            If Math.Round(r.Next(0, 2)) = 0 Then
                c = Chr(Math.Round(r.Next(48, 58)))

            Else
                c = Chr(Math.Round(r.Next(65, 91)))

            End If
            strRand &= c
        Next
        Return strRand
    End Function

    Protected Sub bValidar_Click(sender As Object, e As EventArgs)
        pVerificaMail.Visible = False
        Dim sqlIngreso As String = Session("sqlIngreso") & " "
        If sqlIngreso.Length < 10 Or sqlIngreso.IndexOf("insert") < 0 Then
            lErrorresU.Text = "Hubo un error con el código. Por favor, trate de generar un nuevo código."
            lErrorresU.Visible = True
            Exit Sub
        End If

        If tValidar.Text.Trim.ToUpper <> Session("Codigo") Then
            lCodigo.Visible = True
            Exit Sub
        End If

        If SQL_Accion(sqlIngreso) = False Then
            lErrorresU.Text = "Hubo un error al tratar de validar el código. Por favor, trate de generar un nuevo código."
            lErrorresU.Visible = True
            Exit Sub
        End If

        Session("idUsuario") = VNum(LeeUnCampo("select top 1 idUsuario from usuarios where Usuario='" & Session("Usuario") & "' and Pass='" & Session("Password") & "' ", "idusuario"))
        lBienvenido.Text = "Bienvenido " & Session("ApellidoYNombre") & "!"

        Dim mensaje As String, xusuario As String = Session("ApellidoYNombre"),
        en As String = Chr(13) & Chr(10)
        mensaje = "Bienvenido " & xusuario & " a Camisetas Nani!." & en & en & en _
            & "Te damos una cordial bienvenida a la comunidad de Camisetas Nani!." & en & en _
            & "Tu usuario es " & """" & Session("Usuario") & """" & en & en _
            & "Tu clave es " & """" & Session("Password") & """" & en & en _
            & "Y ya podés loguearte para ver tus datos!!." & en & en

        LimpiarRegistroU()
        pRegistrarse.Visible = False
        pBienvenido.Visible = True
        bOkBack.Focus()
    End Sub

    Protected Sub bRegresarRegistro_Click(sender As Object, e As EventArgs)
        pVerificaMail.Visible = False
        tValidar.Text = ""
        pRegistrarse.Visible = True
    End Sub
End Class