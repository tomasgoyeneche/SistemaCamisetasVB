<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Default.aspx.vb" Inherits="Sistema._Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Sistema</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-QWTKZyjpPEjISv5WaRU9OFeRpok6YctnYmDr5pNlyT2bRjXh0JMhjY6hW+ALEwIH" crossorigin="anonymous" />
    <style type="text/css">

        /* ESTILOS*/

        /* Estilo del Form*/

        #form1 {
            width: 767px;
            margin: auto auto;
            margin-top: auto;
            margin-bottom: auto;
        }

        /* Imagen de fondo de toda la app */

        .background-panel {
            background: rgba(0, 0, 0, 0.9) url('/imagenes/fondocamiseta.jpeg') no-repeat center center;
            background-size: cover;
            background-blend-mode: darken;
        }

        /* Imagen Del Logo */

        .background-logo {
            background-image: url('/imagenes/logocamiseta.jpg'); /* Ruta relativa a la raíz de la aplicación */
            background-size: cover;
            background-position: center;
            background-repeat: no-repeat;
            background-color: rgba(0, 0, 0, 0.9);
            background-blend-mode: darken;
        }


        /* Acomodadores de Codigo */

        .div_campo {
            margin-top: 20px;
            display: flex;
            justify-content: space-between;
        }

        .campo-80 {
            flex: 0 0 40%; /* Ocupa el 80% del ancho disponible */
        }

        .campo-60 {
            flex: 0 0 60%; /* Ocupa el 80% del ancho disponible */
        }

        .campo-20 {
            flex: 0 0 20%; /* Ocupa el 20% del ancho disponible */
        }


        .div_login {
            margin-top: 40px;
            display: flex;
            justify-content: space-around;
            align-content: center;
            align-items: center;
        }

        .campo_centro {
            margin-top: 20px;
            display: flex;
            flex-direction: column;
            align-items: center;
        }

        .campo_cambiodatos {
            height: 400px;
            display: flex;
            flex-direction: column;
            align-items: center;
            justify-content: center;
        }

        .espacio_toppedido {
            margin-top: 150px;
        }


        /* BOTONES */


        /* Boton Usuario Administrador */

        .div_boton_usuadm {
            margin-top: 40px;
            width: 100%;
            display: flex;
            align-items: center;
            justify-content: space-around;
        }

        /* Boton Para Volver Atras */

        .btn-customback {
            background-color: #6c757d;
            color: white;
            width: 200px;
            height: 50px;
            font-size: 16px;
            font-weight: bold;
            border-radius: 25px;
            border: none;
            transition: background-color 0.3s ease-in-out, transform 0.3s ease-in-out;
        }

            .btn-customback:hover {
                background-color: #5a6268;
                transform: scale(1.05);
            }

            .btn-customback:active {
                background-color: #545b62;
                transform: scale(1);
            }

        /* Boton Success */

        .btn-customsucc {
            background-color: forestgreen;
            color: white;
            width: 200px;
            height: 50px;
            font-size: 16px;
            font-weight: bold;
            border-radius: 25px;
            border: none;
            transition: background-color 0.3s ease-in-out, transform 0.3s ease-in-out;
        }

            .btn-customsucc:hover {
                background-color: green;
                transform: scale(1.05);
            }

            .btn-customsucc:active {
                background-color: #545b62;
                transform: scale(1);
            }

         /* Boton Para Funciones ADMIN */

        .btn-customped {
            background-color: midnightblue;
            color: white;
            width: 200px;
            height: 50px;
            font-size: 16px;
            font-weight: bold;
            border-radius: 25px;
            border: none;
            transition: background-color 0.3s ease-in-out, transform 0.3s ease-in-out;
        }

            .btn-customped:hover {
                background-color: mediumblue;
                transform: scale(1.05);
            }

            .btn-customped:active {
                background-color: #545b62;
                transform: scale(1);
            }



         /* Boton Para Funciones Usuarios */

        .btn-customAU {
            background-color: ghostwhite;
            color: black;
            width: 200px;
            height: 50px;
            font-size: 16px;
            font-weight: bold;
            border-radius: 25px;
            border: none;
            transition: background-color 0.3s ease-in-out, transform 0.3s ease-in-out;
        }

            .btn-customAU:hover {
                background-color: white;
                color: black;
                transform: scale(1.05);
            }

            .btn-customAU:active {
                background-color: #545b62;
                transform: scale(1);
            }



        /* margin top para el responsive*/

        .table-responsive {
            margin-top: 20px;
        }

        /* Botones de la grilla */

        .btn-grid {
            padding: 5px 10px;
            font-size: 12px;
        }

        .btn-ver {
            background-color: #ffc107;
            border-color: #ffc107;
        }

        .btn-anular {
            background-color: #dc3545;
            border-color: #dc3545;
        }

        .btn-desp {
            background-color: #28a745;
            border-color: #28a745;
        }

    </style>
</head>

<body>
    <form id="form1" runat="server">
        <asp:Panel ID="pPortada" CssClass="background-logo" runat="server" Height="100%" ForeColor="WhiteSmoke">
            <div class="campo_cambiodatos">
                <h1>Camisetas Nani</h1>
                <asp:Button ID="bIngresoLogo" type="button" CssClass="btn btn-customAU mt-4" runat="server" Text="Ingresar" OnClick="bIngresoLogo_Click" />
            </div>
        </asp:Panel>

        <asp:Panel ID="pAdminUsuLog" runat="server" Font-Size="Large" CssClass="background-panel" ForeColor="WhiteSmoke" BorderStyle="Solid" Visible="False">
            <div class="campo_cambiodatos">
                <asp:Label ID="Label8" runat="server" Text="Seleccione Login como Administrador o como Usuario" Font-Bold="true" Font-Size="X-Large"></asp:Label>

                <div class="div_boton_usuadm">
                    <asp:Button ID="bBotonLogUsu" type="button" CssClass="btn btn btn-customAU" runat="server" Text="Usuario" OnClick="bBotonLogUsu_Click" Height="150%" Width="30%" />
                    <asp:Button ID="bBotonLogAdm" type="button" CssClass="btn btn-customped" runat="server" Text="Administrador" OnClick="bBotonLogAdm_Click" Height="150%" Width="30%" />
                </div>

                <div class="campo_centro espacio_toppedido">
                    <asp:Button ID="canVolUsuAdm" type="button" CssClass="btn btn-customback" runat="server" Text="Cancelar Y Volver" OnClick="canVolUsuAdm_Click" Width="200%" />

                </div>
            </div>
        </asp:Panel>

        <asp:Panel ID="pLogin" runat="server" Height="100%" Font-Bold="true" Font-Size="Large" CssClass="background-panel" ForeColor="WhiteSmoke" BorderStyle="Solid" Visible="False">

            <asp:Label ID="lVersion" runat="server" Font-Bold="true" Font-Size="Large" ForeColor="#999999" Text="Version"></asp:Label>

            <div class="campo_centro">
                <asp:Label ID="Label104" runat="server" Font-Bold="true" Font-Size="X-Large" Text="Ingresa tu Usuario y Clave, y oprimi Entrar!"></asp:Label>

            </div>

            <div class="div_login">
                <asp:Label ID="Label107" runat="server" Text="Usuario:"></asp:Label>
                <asp:TextBox ID="tUsuario" runat="server" Width="170px" CssClass="form-control" Height="40px" BackColor="White"></asp:TextBox>

                <asp:Label ID="Label108" runat="server" Text="Clave:"></asp:Label>
                <asp:TextBox ID="tClave" runat="server" Width="170px" Height="40px" CssClass="form-control" BackColor="White" TextMode="Password"></asp:TextBox>


                <asp:Button ID="Button2" type="button" CssClass="btn btn-customAU" runat="server" Text="Login" Width="15%" OnClick="Button2_Click" />

            </div>

            <div class="campo_centro">
                <asp:Label ID="lErrorLogin" runat="server" Text="lErrorLogin" Visible="False" ForeColor="red"></asp:Label>
            </div>

            <div class="div_login">
                <asp:Button ID="bRecClave" type="button" CssClass="btn btn-customAU" runat="server" Text="Recuperar Clave" Height="150%" Width="40%" OnClick="bRecClave_Click" />



                <asp:Label ID="lReenviarClave" runat="server" Text="[Reenviar Clave]" Visible="False"></asp:Label>
            </div>

            <div class="div_login">
                <asp:Button ID="bVologUsuAdm" type="button" CssClass="btn btn-customback mb-4" runat="server" Text="Cancelar Y Volver" Width="50%" OnClick="bVologUsuAdm_Click" />


            </div>
        </asp:Panel>


        <asp:Panel ID="pLoginMenu" runat="server" Height="100%" Font-Bold="true" CssClass="background-panel" ForeColor="WhiteSmoke" Font-Size="Large" BorderStyle="Solid" Visible="false">
            <div class="campo_cambiodatos">
                <asp:Label ID="Label5" runat="server" Text="Registrate o Ingresa en caso de tener cuenta" Font-Bold="true" Font-Size="X-Large"></asp:Label>

                <div class="div_boton_usuadm">
                    <asp:Button ID="bRegisLoginMenu" type="button" CssClass="btn btn-customsucc" runat="server" Text="Registrate" OnClick="bRegisLoginMenu_Click" />
                    <asp:Button ID="bLogLoginMenu" type="button" CssClass="btn btn-customAU" runat="server" Text="Ingresa" OnClick="bLogLoginMenu_Click" />
                </div>

                <div class="campo_centro espacio_toppedido">
                    <asp:Button ID="bCanVolLoginMenu" type="button" CssClass="btn btn-customback" runat="server" Text="Cancelar Y Volver" OnClick="bCanVolLoginMenu_Click" />

                </div>
            </div>



        </asp:Panel>



        <asp:Panel ID="pRegistrarse" runat="server" BorderColor="#66CCFF" Height="100%" Visible="false" CssClass="background-panel" ForeColor="WhiteSmoke" Font-Size="Large">

            <div class="div_Registro">
                <asp:Label ID="Label2" runat="server" Text="Para poder registrarte debes ser mayor de 18 años, residir en Argentina y contar con documento válido en Argentina que acredite tu identidad."></asp:Label>
                <br />
                Solo se puede hacer un registro por documento. Los datos deben ser reales, correctos y vigentes.<br />
                Todos los datos a continuación (menos Origen) son obligatorios:
                <div class="div_campo">
                    <div class="campo campo-80">
                        <asp:Label runat="server" Text="Tu/s Nombres/s"></asp:Label>
                    </div>
                    <div class="campo campo-80">
                        <asp:TextBox ID="tNombreU" runat="server" CssClass="form-control" MaxLength="20"></asp:TextBox>
                    </div>
                    <div class="campo campo-20">
                        <asp:Label ID="lENombreU" runat="server" Text="lENombreU" Font-Bold="true" Visible="False" ForeColor="Red"></asp:Label>
                    </div>
                </div>
                <div class="div_campo">
                    <div class="campo campo-80">
                        <asp:Label runat="server" Text="Apellido/s"></asp:Label>
                    </div>
                    <div class="campo campo-80">
                        <asp:TextBox ID="tApellidoU" runat="server" CssClass="form-control" MaxLength="20"></asp:TextBox>
                    </div>
                    <div class="campo campo-20">
                        <asp:Label ID="lEApellidoU" runat="server" Text="lEApellidoU" Font-Bold="true" Visible="False" ForeColor="Red"></asp:Label>
                    </div>
                </div>
                <div class="div_campo">
                    <div class="campo campo-80">
                        <asp:Label runat="server" Text="Tipo Doc."></asp:Label>
                    </div>
                    <div class="campo campo-60">
                        <asp:DropDownList ID="dTDocU" CssClass="form-select" Font=" X-Large" Width="65%" runat="server">
                            <asp:ListItem Value="DNI">Doc. Nacional de Identidad</asp:ListItem>
                            <asp:ListItem Value="LC">Libreta Civíca</asp:ListItem>
                            <asp:ListItem Value="LE">Libreta de Enrolamiento</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>

                <div class="div_campo">
                    <div class="campo campo-80">
                        <asp:Label runat="server" Text="N de Doc.(sin puntos ni espacios):"></asp:Label>
                    </div>
                    <div class="campo campo-80">
                        <asp:TextBox ID="tDocU" runat="server" CssClass="form-control" MaxLength="8"></asp:TextBox>
                    </div>
                    <div class="campo campo-20">
                        <asp:Label ID="lEDocU" runat="server" Text="lEDocU" Font-Bold="true" Visible="False" ForeColor="Red"></asp:Label>
                    </div>
                </div>
                <div class="div_campo">
                    <div class="campo campo-80">
                        <asp:Label runat="server" Text="Fecha Nac.:(ddmmaa)"></asp:Label>
                    </div>
                    <div class="campo campo-80">
                        <asp:TextBox ID="tF_Nac" runat="server" CssClass="form-control" MaxLength="6"></asp:TextBox>
                        <asp:Label ID="lEdad" runat="server" Text="0"></asp:Label>
                    </div>
                    <div class="campo campo-20">
                        <asp:Label ID="lEFNac" runat="server" Text="lEFNac" Font-Bold="true" Visible="False" ForeColor="Red"></asp:Label>
                    </div>
                </div>
                <div class="div_campo">
                    <div class="campo campo-80">
                        <asp:Label runat="server" Text="Email válido para notificaciones:"></asp:Label>
                    </div>
                    <div class="campo campo-80">
                        <asp:TextBox ID="tEmailU" runat="server" CssClass="form-control" Height="40px" Width="281px" MaxLength="70" Rows="2" TextMode="MultiLine"></asp:TextBox>
                    </div>
                    <div class="campo campo-20">
                        <asp:Label ID="lEEmailU" runat="server" Text="lEEmailU" Font-Bold="true" Visible="False" ForeColor="Red"></asp:Label>
                    </div>
                </div>

                <div class="div_campo">
                    <div class="campo campo-80">
                        <asp:Label runat="server" Text="Provincia:"></asp:Label>
                    </div>
                    <div class="campo campo-60">
                        <asp:DropDownList ID="didProvU" CssClass="form-select" runat="server" Font-Size="X-Large" Width="276px">
                            <asp:ListItem Value="1">Buenos Aires</asp:ListItem>
                            <asp:ListItem Value="2">Catamarca</asp:ListItem>
                            <asp:ListItem Value="3">Chaco</asp:ListItem>
                            <asp:ListItem Value="4">Chubut</asp:ListItem>
                            <asp:ListItem Value="5">Cordoba</asp:ListItem>
                            <asp:ListItem Value="6">Corrientes</asp:ListItem>
                            <asp:ListItem Value="7">Entre Ríos</asp:ListItem>
                            <asp:ListItem Value="8">Formosa</asp:ListItem>
                            <asp:ListItem Value="9">Jujuy</asp:ListItem>
                            <asp:ListItem Value="10">La Pampa</asp:ListItem>
                            <asp:ListItem Value="11">La Rioja</asp:ListItem>
                            <asp:ListItem Value="12">Mendoza</asp:ListItem>
                            <asp:ListItem Value="13">Misiones</asp:ListItem>
                            <asp:ListItem Value="14">Neuquen</asp:ListItem>
                            <asp:ListItem Value="15">Rio Negro</asp:ListItem>
                            <asp:ListItem Value="16">Salta</asp:ListItem>
                            <asp:ListItem Value="17">San Juan</asp:ListItem>
                            <asp:ListItem Value="18">San Luis</asp:ListItem>
                            <asp:ListItem Value="19">Santa Cruz</asp:ListItem>
                            <asp:ListItem Value="20">Santiago del Estero</asp:ListItem>
                            <asp:ListItem Value="21">Tierra del Fuego</asp:ListItem>
                            <asp:ListItem Value="22">Tucuman</asp:ListItem>
                            <asp:ListItem Value="23">CABA</asp:ListItem>
                            <asp:ListItem Value="24">Santa Fe</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>

                <div class="div_campo">
                    <div class="campo campo-80">
                        <asp:Label runat="server" Text="Localidad:"></asp:Label>
                    </div>
                    <div class="campo campo-80">
                        <asp:TextBox ID="tLocalidadU" runat="server" CssClass="form-control" MaxLength="25" Width="276px"></asp:TextBox>

                    </div>
                    <div class="campo campo-20">
                        <asp:Label ID="lELocalidadU" runat="server" Text="lELocalidadU" Font-Bold="true" Visible="False" ForeColor="Red"></asp:Label>
                    </div>
                </div>

                <div class="div_campo">
                    <div class="campo campo-80">
                        <asp:Label runat="server" Text="Direccion"></asp:Label>
                    </div>
                    <div class="campo campo-80">
                        <asp:TextBox ID="tDireccionU" runat="server" Height="54px" CssClass="form-control" Width="280px" MaxLength="100" Rows="2" TextMode="MultiLine"></asp:TextBox>

                    </div>
                    <div class="campo campo-20">
                        <asp:Label ID="lEDireccionU" runat="server" Text="lEDireccionU" Font-Bold="true" Visible="False" ForeColor="Red"></asp:Label>
                    </div>
                </div>

                <div class="div_campo">
                    <div class="campo campo-80">
                        <asp:Label runat="server" Text="Teléfono (agregue característica):"></asp:Label>
                    </div>
                    <div class="campo campo-80">
                        <asp:TextBox ID="tTelefonosU" runat="server" CssClass="form-control" MaxLength="25" Width="274px"></asp:TextBox>
                    </div>
                    <div class="campo campo-20">
                        <asp:Label ID="lETelefonosU" runat="server" Text="lETelefonosU" Font-Bold="true" Visible="False" ForeColor="Red"></asp:Label>
                    </div>
                </div>

                <div class="div_campo">
                    <div class="campo campo-80">
                        <asp:Label runat="server" Text="Usuario:"></asp:Label>
                    </div>
                    <div class="campo campo-80">
                        <asp:TextBox ID="tUsuarioU" runat="server" CssClass="form-control" MaxLength="10" Width="151px"></asp:TextBox>
                    </div>
                    <div class="campo campo-20">
                        <asp:Label ID="lEUsuarioU" runat="server" Text="lEUsuarioU" Font-Bold="true" Visible="False" ForeColor="Red"></asp:Label>
                    </div>
                </div>

                <div class="div_campo">
                    <div class="campo campo-80">
                        <asp:Label runat="server" Text="Clave:"></asp:Label>
                    </div>
                    <div class="campo campo-80">
                        <asp:TextBox ID="tPassU" runat="server" CssClass="form-control" Width="151px" MaxLength="10" TextMode="Password"></asp:TextBox>

                    </div>
                    <div class="campo campo-20">
                        <asp:Label ID="lEPassU" runat="server" Text="lEPassU" Font-Bold="true" Visible="False" ForeColor="Red"></asp:Label>

                    </div>
                </div>

                <div class="div_campo">
                    <div class="campo campo-80">
                        <asp:Label runat="server" Text="Repetir Clave:"></asp:Label>
                    </div>
                    <div class="campo campo-80">
                        <asp:TextBox ID="tPass2U" runat="server" CssClass="form-control" Width="151px" MaxLength="10" TextMode="Password"></asp:TextBox>

                    </div>
                    <div class="campo campo-20">
                        <asp:Label ID="lEPass2U" runat="server" Text="lEPass2U" Font-Bold="true" Visible="False" ForeColor="Red"></asp:Label>

                    </div>
                </div>
                <div class="div_campo">
                    <asp:Label ID="lErroresU" runat="server" Text="lErroresU" Font-Bold="true" Visible="False" ForeColor="Red"></asp:Label>
                </div>
                <div class="div_login pb-2">

                    <asp:Button ID="bRegistrarseSuccess" type="button" CssClass="btn btn-customsucc" runat="server" Text="Registrarse" OnClick="bRegistrarseSuccess_Click" />

                    <asp:Button ID="bRegistrarseBack" type="button" CssClass="btn btn-customback" runat="server" Text="Cancelar Y Volver" OnClick="bRegistrarseBack_Click" />

                </div>


            </div>
        </asp:Panel>


        <asp:Panel ID="pBienvenido" runat="server" BorderColor="#66CCFF" BorderStyle="Groove" Height="100%" CssClass="background-panel" ForeColor="WhiteSmoke" Font-Size="Large" Visible="false">
            <div>
                <asp:Label ID="lBienvenido" CssClass="campo_centro" runat="server" Text="Bienvenido/a!!" Font-Bold="true" Font-Size="X-Large"></asp:Label>
                <div>
                    <p>Ya Estas Anotado En Camisetas Nani!!!!</p>
                    <p>Reglas y Condiciones de Camisetas Nani:</p>
                    <p>1- Realiza Pedidos Reesponsablemente</p>
                    <p>2- En Caso De No Hacerlo Seras Sancionado Con La Desactivacion De Tu Usuario</p>
                    <p>3- No Cambies Tus Datos De Usuario Si No Es Necesario </p>
                    <p>4- No compartas tu contraseña</p>
                    <p>5- Usa la app responsablemente</p>
                    <p>Nos Alegra Mucho Que Estes Con Nosotros. El Equipo De Camisetas Nani</p>
                </div>
                <div class="campo_centro pb-2">
                    <asp:Button ID="bOkBack" type="button" CssClass="btn btn-customback" runat="server" Text="Todo Ok!! Volve al login" OnClick="bOkBack_Click" />

                </div>
            </div>
        </asp:Panel>

        <asp:Panel ID="pAreaUsuario" runat="server" BorderColor="#66CCFF" BorderStyle="Groove" Visible="false" CssClass="background-panel" ForeColor="WhiteSmoke" Font-Size="Large">
            <asp:Label ID="lBienvenidoAreaU" class="campo_centro" runat="server" Text="Bienvenido/a!!" Font-Bold="true" Font-Size="X-Large"></asp:Label>
            <div class="campo_centro">
                <asp:Button ID="bAhoraQHago" CssClass="btn btn-customAU " runat="server" Text="¿Ahora Que Hago?" Width="55%" OnClick="bAhoraQHago_Click" />
            </div>
            <div class="campo_centro">
                <p>Desde aca vas a poder hacer varias cosas relacionadas con tu cuenta en ASP .NET</p>
                <asp:Button ID="bCambioProducto" CssClass="btn btn-customped " runat="server" Text="ABM de Camisetas" Width="65%" OnClick="bCambioProducto_Click" />
                <asp:Button ID="bABMAdmin" CssClass="btn btn-customped mt-4 " runat="server" Text="ABM de Administradores" Width="65%" OnClick="bABMAdmin_Click" />
                <asp:Button ID="bDesEliUsu" CssClass="btn btn-customped mt-4 " runat="server" Text="Activar/Eliminar Usuario" Width="65%" OnClick="bDesEliUsu_Click" />
                <asp:Button ID="bModDatos" CssClass="btn btn-customAU mt-4 " runat="server" Text="Modificar Datos Personales" Width="65%" OnClick="bModDatos_Click" />
                <asp:Button ID="bPedidosAFab" CssClass="btn btn-customAU mt-4 " runat="server" Text="Pedidos a Fábrica" Width="65%" OnClick="bPedidosAFab_Click" />

                <asp:Button ID="Button1" CssClass="btn btn-customback mt-4 mb-2 " runat="server" Text="Terminar y volver" Width="50%" OnClick="Button1_Click" />



            </div>
        </asp:Panel>

        <asp:Panel ID="pAhoraQueHago" runat="server" BorderColor="Black" BorderStyle="Groove" Height="100%" Visible="false" CssClass="background-panel" ForeColor="WhiteSmoke" Font-Size="Large">
            <asp:Label ID="Label3" class="campo_centro" runat="server" Text="Te Cuento un Poco!" Font-Bold="true" Font-Size="X-Large"></asp:Label>
            <div>
                <p>Funcionamiento de la aplicacion</p>
                <p>Esta es una tienda de camisetas vas a poder realizar pedidos en caso de ser usuario y despacharlos en caso de ser administrador</p>
                <p>Funciones de Usuario:</p>
                <p>1- Realizar Pedidos</p>
                <p>2- Modificar Tus Datos</p>
                <p>3- Ver Tus Pedidos Realizados</p>
                <p>Funciones de Administrador:</p>
                <p>1- Ver Todos Los Pedidos</p>
                <p>2- Modificar Tus Datos</p>
                <p>3- ABM De Camisetas y De Administradores</p>
                <p>4- Despachar Los Pedidos Solicitados Por Los Administradores</p>
                <p>5- Eliminar o Desactivar Usuarios</p>
            </div>
            <div class="campo_centro">
                <asp:Button ID="bVolverLogPR" CssClass="btn btn-customback mb-2 " runat="server" Text="Terminar y volver" Width="50%" OnClick="bVolverLogPR_Click" />
            </div>
        </asp:Panel>


        <asp:Panel ID="pCambiarDatosPersonalesU" runat="server" Height="100%" Visible="false" BorderColor="Black" BorderStyle="Groove" CssClass="background-panel" ForeColor="WhiteSmoke" Font-Size="Large" Font-Bold="true">
            <div class="campo_centro">
                <asp:Label ID="Label1" runat="server" Text="Cambia tus datos personales" Font-Bold="true" Font-Size="X-Large"></asp:Label>
            </div>

            <div class="div_campo">
                <div class="campo campo-80">
                    <asp:Label runat="server" Text="Email válido para notificaciones:"></asp:Label>
                </div>
                <div class="campo campo-80">
                    <asp:TextBox ID="ctEmailU" runat="server" Height="40px" CssClass="form-control" Width="281px" MaxLength="70" Rows="2" TextMode="MultiLine"></asp:TextBox>
                </div>
                <div class="campo campo-20">
                    <asp:Label ID="clEEmailU" runat="server" Text="clEEmailU" Font-Bold="true" Visible="False" ForeColor="Red"></asp:Label>
                </div>
            </div>

            <div class="div_campo">
                <div class="campo campo-80">
                    <asp:Label runat="server" Text="Provincia:"></asp:Label>
                </div>
                <div class="campo campo-60">
                    <asp:DropDownList ID="cdProvinciaU" runat="server" CssClass="form-select" Font-Size="X-Large" Width="276px">
                        <asp:ListItem Value="1">Buenos Aires</asp:ListItem>
                        <asp:ListItem Value="2">Catamarca</asp:ListItem>
                        <asp:ListItem Value="3">Chaco</asp:ListItem>
                        <asp:ListItem Value="4">Chubut</asp:ListItem>
                        <asp:ListItem Value="5">Cordoba</asp:ListItem>
                        <asp:ListItem Value="6">Corrientes</asp:ListItem>
                        <asp:ListItem Value="7">Entre Ríos</asp:ListItem>
                        <asp:ListItem Value="8">Formosa</asp:ListItem>
                        <asp:ListItem Value="9">Jujuy</asp:ListItem>
                        <asp:ListItem Value="10">La Pampa</asp:ListItem>
                        <asp:ListItem Value="11">La Rioja</asp:ListItem>
                        <asp:ListItem Value="12">Mendoza</asp:ListItem>
                        <asp:ListItem Value="13">Misiones</asp:ListItem>
                        <asp:ListItem Value="14">Neuquen</asp:ListItem>
                        <asp:ListItem Value="15">Rio Negro</asp:ListItem>
                        <asp:ListItem Value="16">Salta</asp:ListItem>
                        <asp:ListItem Value="17">San Juan</asp:ListItem>
                        <asp:ListItem Value="18">San Luis</asp:ListItem>
                        <asp:ListItem Value="19">Santa Cruz</asp:ListItem>
                        <asp:ListItem Value="20">Santiago del Estero</asp:ListItem>
                        <asp:ListItem Value="21">Tierra del Fuego</asp:ListItem>
                        <asp:ListItem Value="22">Tucuman</asp:ListItem>
                        <asp:ListItem Value="23">CABA</asp:ListItem>
                        <asp:ListItem Value="24">Santa Fe</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>

            <div class="div_campo">
                <div class="campo campo-80">
                    <asp:Label runat="server" Text="Localidad:"></asp:Label>
                </div>
                <div class="campo campo-80">
                    <asp:TextBox ID="ctLocalidadU" runat="server" CssClass="form-control" MaxLength="25" Width="276px"></asp:TextBox>

                </div>
                <div class="campo campo-20">
                    <asp:Label ID="clELocalidadU" runat="server" Text="clELocalidadU" Font-Bold="true" Visible="False" ForeColor="Red"></asp:Label>
                </div>
            </div>

            <div class="div_campo">
                <div class="campo campo-80">
                    <asp:Label runat="server" Text="Direccion"></asp:Label>
                </div>
                <div class="campo campo-80">
                    <asp:TextBox ID="ctDireccionU" runat="server" Height="54px" Width="280px" CssClass="form-control" MaxLength="100" Rows="2" TextMode="MultiLine"></asp:TextBox>

                </div>
                <div class="campo campo-20">
                    <asp:Label ID="clEDireccionU" runat="server" Text="clEDireccionU" Font-Bold="true" Visible="False" ForeColor="Red"></asp:Label>
                </div>
            </div>

            <div class="div_campo">
                <div class="campo campo-80">
                    <asp:Label runat="server" Text="Teléfono (agregue característica):"></asp:Label>
                </div>
                <div class="campo campo-80">
                    <asp:TextBox ID="ctTelefonosU" runat="server" CssClass="form-control" MaxLength="25" Width="274px"></asp:TextBox>
                </div>
                <div class="campo campo-20">
                    <asp:Label ID="clETelefonosU" runat="server" Text="clETelefonosU" Font-Bold="true" Visible="False" ForeColor="Red"></asp:Label>
                </div>
            </div>

            <div class="div_campo">
                <div class="campo campo-80">
                    <asp:Label runat="server" Text="DNI:"></asp:Label>
                </div>
                <div class="campo campo-80">
                    <asp:TextBox ID="ctDocU" runat="server" CssClass="form-control" Width="151px" MaxLength="8"></asp:TextBox>

                </div>
                <div class="campo campo-20">
                    <asp:Label ID="clEDocU" runat="server" Text="clEDocU" Font-Bold="true" Visible="False" ForeColor="Red"></asp:Label>

                </div>
            </div>

            <div class="div_campo">
                <div class="campo campo-80">
                    <asp:Label runat="server" Text="Usuario:"></asp:Label>
                </div>
                <div class="campo campo-80">
                    <asp:TextBox ID="ctUsuarioU" runat="server" CssClass="form-control" MaxLength="10" Width="151px"></asp:TextBox>
                </div>
                <div class="campo campo-20">
                    <asp:Label ID="clEUsuarioU" runat="server" Text="clEUsuarioU" Font-Bold="true" Visible="False" ForeColor="Red"></asp:Label>
                </div>
            </div>

            <div class="div_campo">
                <div class="campo campo-80">
                    <asp:Label runat="server" Text="Clave:"></asp:Label>
                </div>
                <div class="campo campo-80">
                    <asp:TextBox ID="ctPassU" runat="server" Width="151px" CssClass="form-control" MaxLength="10" TextMode="Password"></asp:TextBox>

                </div>
                <div class="campo campo-20">
                    <asp:Label ID="clEPassU" runat="server" Text="clEPassU" Font-Bold="true" Visible="False" ForeColor="Red"></asp:Label>

                </div>
            </div>

            <div class="campo_centro">
                <asp:Label ID="cError" runat="server" Text="Error" Font-Bold="true" Visible="False" ForeColor="Red"></asp:Label>
            </div>

            <div class="div_login">
                <asp:Button ID="bCambDatos" type="button" CssClass="btn btn-customsucc" runat="server" Text="Cambiar Datos" OnClick="bCambDatos_Click" />


                <asp:Button ID="canVolCam" CssClass="btn btn-customback mb-2 " runat="server" Text="Cancelar y volver" OnClick="canVolCam_Click" />
            </div>

        </asp:Panel>

        <asp:Panel ID="pCambioExitoso" runat="server" Height="100%" Visible="false" CssClass="background-panel" ForeColor="WhiteSmoke" Font-Size="Large" Font-Bold="true" BorderColor="Black" BorderStyle="Groove">
            <div class="campo_cambiodatos">
                <asp:Label ID="Label4" runat="server" Text="Tus datos han sido cambiados correctamente" Font-Bold="true" Font-Size="X-Large"></asp:Label>
                <asp:Button ID="bVolverUsu" CssClass="btn btn-customback mt-5 " runat="server" Width="80%" Text="Volver a tu area de usuario" OnClick="bVolverUsu_Click" />

            </div>
        </asp:Panel>

        <asp:Panel ID="pPedidos" runat="server" Height="100%" Font-Size="Large" Font-Bold="true" BorderColor="Black" BorderStyle="Solid" Visible="false" CssClass="background-panel" ForeColor="WhiteSmoke">

            <h2>Menú de Pedidos</h2>
            <div class="campo_centro">
                <asp:Button ID="bNuevoPedido" CssClass="btn btn-customped mb-2 " runat="server" Text="Nuevo Pedido" Width="45%" OnClick="bNuevoPedido_Click" />

                <asp:Button ID="bTodosLosPedidos" CssClass="btn btn-customped mt-2 " runat="server" Text="Todos los Pedidos" Width="45%" OnClick="bTodosLosPedidos_Click" />


            </div>
            <div class="mt-5 campo_centro">
                <asp:Button ID="bCanVolPed" CssClass="btn btn-customback mb-2" runat="server" Text="Cancelar y volver" OnClick="bCanVolPed_Click" />

            </div>

        </asp:Panel>

        <asp:Panel ID="pNuevoPedido" runat="server" Height="100%" ForeColor="WhiteSmoke" CssClass="background-panel" Font-Size="Large" Font-Bold="true" BorderColor="Black" Visible="false" BorderStyle="Solid">
            <h2>Nuevo Pedido a Fabrica</h2>
            <div class="mt-2">
                <asp:Button ID="bInstrucciones" runat="server" Text="Abrir Instrucciones" CssClass="btn btn-customAU" Width="22%" OnClick="bInstrucciones_Click1" />
                <asp:Label ID="lInstrucciones" runat="server" Text="Instrucciones"></asp:Label>
            </div>
            <div class="mt-2">
                <p>Seleccione el Equipo</p>
                <asp:DropDownList ID="dCamisetas" runat="server" CssClass="form-select" Font-Size="X-Large" Width="90%" AutoPostBack="True" OnSelectedIndexChanged="dHelados_SelectedIndexChanged">
                </asp:DropDownList>

            </div>
            <div class="mt-2">
                <p>Seleccione el Pais</p>
                <asp:DropDownList ID="dPais" runat="server" CssClass="form-select" Font-Size="X-Large" Width="90%" AutoPostBack="True" OnSelectedIndexChanged="dPais_SelectedIndexChanged">
                </asp:DropDownList>

            </div>
            <div class="div_campo">
                <asp:Label ID="lCositaAgregar" runat="server" Text="Agregar" Font-Size="Medium"></asp:Label>
                <asp:Label ID="lCosaAgregar" runat="server" Text="Label" ForeColor="WhiteSmoke"></asp:Label>
                <asp:Label ID="lQueEs" runat="server" Text="Label" Font-Size="Medium"></asp:Label>
            </div>
            <div class="div_campo">
                <asp:Label ID="Label6" runat="server" Text="Cantidad de Camisetas" Font-Size="Medium"></asp:Label>
                <asp:DropDownList ID="tCantLatas" runat="server" CssClass="form-select" Width="10%" Font-Size="X-Large">
                    <asp:ListItem Value="1">1</asp:ListItem>
                    <asp:ListItem Value="2">2</asp:ListItem>
                    <asp:ListItem Value="3">3</asp:ListItem>
                    <asp:ListItem Value="4">4</asp:ListItem>
                    <asp:ListItem Value="5">5</asp:ListItem>
                    <asp:ListItem Value="6">6</asp:ListItem>
                    <asp:ListItem Value="7">7</asp:ListItem>
                    <asp:ListItem Value="8">8</asp:ListItem>
                    <asp:ListItem Value="9">9</asp:ListItem>
                    <asp:ListItem Value="10">10</asp:ListItem>
                    <asp:ListItem Value="11">11</asp:ListItem>
                    <asp:ListItem Value="12">12</asp:ListItem>
                </asp:DropDownList>
                <asp:Button ID="bAgregarLista" CssClass="btn btn-customped mb-2 " runat="server" Text="Agregar al Pedido" Width="25%" OnClick="bAgregarLista_Click" />

            </div>
            <p>
                <asp:Label ID="Label7" runat="server" Text="Esta es la lista de su pedido" Font-Size="Medium"></asp:Label>


            </p>
            <div class="table-responsive">
                <asp:GridView ID="gListaPedido" runat="server" Width="100%" CssClass="table table-striped" Visible="false" AutoGenerateColumns="False" OnRowCommand="gListaPedido_RowCommand">
                    <AlternatingRowStyle BackColor="#CCCCCC" />
                    <Columns>
                        <asp:ButtonField CommandName="Quitar" Text="Quitar" ControlStyle-CssClass="btn btn-grid btn-anular" ButtonType="button"></asp:ButtonField>
                        <asp:BoundField DataField="Item" HeaderText="Item Solicitado"></asp:BoundField>
                        <asp:BoundField DataField="pais" HeaderText="Pais"></asp:BoundField>
                        <asp:BoundField DataField="Cantidad" HeaderText="Cant."></asp:BoundField>
                    </Columns>
                    <FooterStyle BackColor="#CCCCCC" />
                    <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                    <SortedAscendingHeaderStyle BackColor="#808080" />
                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                    <SortedDescendingHeaderStyle BackColor="#383838" />
                </asp:GridView>
            </div>
            <div class="div_login mb-4">
                <asp:Button ID="bBorrarPedido" CssClass="btn btn-danger mb-2 text-black " runat="server" Text="Quitar Todos" Width="25%" OnClick="bBorrarPedido_Click" />

                <asp:Button ID="bEnvPedido" CssClass="btn btn-customped mb-2 " runat="server" Text="ENVIAR PEDIDO" Width="35%" OnClick="bEnvPedido_Click" />
                <asp:Button ID="bCanPedido" CssClass="btn btn-customback mb-2 " runat="server" Text="Cancelar Pedido" OnClick="bCanPedido_Click" />
            </div>
            <asp:Label ID="lErrorPedido" runat="server" Text="Error Pedido" Font-Bold="true" Visible="False" ForeColor="Red"></asp:Label>

        </asp:Panel>

        <asp:Panel ID="pPedidoCreado" runat="server" Height="100%" ForeColor="WhiteSmoke" CssClass="background-panel" Font-Size="Large" Font-Bold="true" BorderColor="Black" BorderStyle="Solid" Visible="false">
            <div class="campo_cambiodatos">
                <asp:Label ID="lPedidoCreado" runat="server" Text="Label" Font-Bold="true" Font-Size="X-Large"></asp:Label>
                <asp:Button ID="bTermVolverPed" CssClass="btn btn-customback mb-2 espacio_toppedido" runat="server" Text="Terminar y volver" Width="50%" OnClick="bTermVolverPed_Click" />
            </div>

        </asp:Panel>

        <asp:Panel ID="pPedidoAnulado" runat="server" Height="100%" ForeColor="WhiteSmoke" CssClass="background-panel" Font-Size="Large" Font-Bold="true" BorderColor="Black" BorderStyle="Solid" Visible="false">
            <div class="campo_cambiodatos">
                <asp:Label ID="lPedidoAnulado" runat="server" Text="Label" Font-Bold="true" Font-Size="X-Large"></asp:Label>
                <asp:Button ID="bTermVolverAnu" CssClass="btn btn-customback mb-2 espacio_toppedido" runat="server" Text="Terminar y volver" Width="50%" OnClick="bTermVolverAnu_Click" />


            </div>

        </asp:Panel>

        <asp:Panel ID="pHistorico" runat="server" Height="100%" Font-Size="Large" Font-Bold="true" BorderColor="Black" BorderStyle="Solid" ForeColor="WhiteSmoke" CssClass="background-panel" Visible="false">
            <h2>Historico de Pedidos y Revisar Estado</h2>
            <div class="campo_centro">
                <asp:Label ID="lErrorHistorico" runat="server" Text="lErrorHistorico" Font-Bold="true" Visible="false" ForeColor="Red"></asp:Label>
            </div>
            <div class="table-responsive">
                <asp:GridView ID="gHistorico" runat="server" Width="100%" CssClass="table table-striped" OnRowCommand="gHistorico_RowCommand" AutoGenerateColumns="False">
                    <Columns>
                        <asp:ButtonField CommandName="Ver" Text="Ver/Editar" ButtonType="Button" ControlStyle-CssClass="btn btn-grid btn-ver"></asp:ButtonField>
                        <asp:ButtonField CommandName="Anular" Text="Anular Pedido" ButtonType="Button" ControlStyle-CssClass="btn btn-grid btn-anular" HeaderText="(solo &quot;Solicitado&quot;)"></asp:ButtonField>
                        <asp:BoundField DataField="NPedido" HeaderText="N&#176; Pedido" ReadOnly="True"></asp:BoundField>
                        <asp:BoundField DataField="fecha" DataFormatString="{0:dd/MM/yyyy HH:mm:tt}" HeaderText="Fecha y Hora" ReadOnly="True"></asp:BoundField>
                        <asp:BoundField DataField="estado" HeaderText="Estado del Pedido"></asp:BoundField>
                        <asp:ButtonField CommandName="Desp" Text="Despachar Pedido" ButtonType="Button" ControlStyle-CssClass="btn btn-grid btn-desp"></asp:ButtonField>
                    </Columns>

                </asp:GridView>
            </div>

            <div class="div_login">
                <asp:Button ID="bActHisto" type="button" CssClass="btn btn-customsucc mb-2" runat="server" Text="Actualizar" Width="40%" OnClick="bActHisto_Click" />

                <asp:Button ID="bTermHisto" CssClass="btn btn-customback mb-2" runat="server" Text="Terminar y volver" Width="40%" OnClick="bTermHisto_Click" />

            </div>

        </asp:Panel>

        <asp:Panel ID="pVerUnPedido" runat="server" Height="100%" Font-Size="Large" Font-Bold="true" BorderColor="Black" BorderStyle="Solid" ForeColor="WhiteSmoke" CssClass="background-panel" Visible="false">
            <asp:Label ID="lDetallePedido" runat="server" Text="Detalle del Pedido N°" Font-Bold="true" Font-Size="XX-Large"></asp:Label>



            <div class="campo_centro table-responsive">
                <asp:GridView ID="gVerUnPedido" runat="server" Width="80%" CssClass="table table-striped" OnRowCommand="gVerUnPedido_RowCommand" AutoGenerateColumns="False">

                    <Columns>
                        <asp:BoundField DataField="Item" HeaderText="Item Solicitado"></asp:BoundField>
                        <asp:BoundField DataField="pais" HeaderText="Pais"></asp:BoundField>
                        <asp:BoundField DataField="Cantidad" HeaderText="Cant."></asp:BoundField>
                        <asp:ButtonField CommandName="Quitar" Text="Quitar" ButtonType="Button" Visible="false" ControlStyle-CssClass="btn btn-grid btn-anular" HeaderText="Acción"></asp:ButtonField>

                    </Columns>

                </asp:GridView>
                <div class="mt-4">
                    <asp:Label ID="lErrorVerUnPedido" runat="server" Text="lErrorVerUnPedido" Font-Bold="true" Visible="false" ForeColor="Red"></asp:Label>

                </div>
                <div class="mt-4">
                    <asp:Button ID="bVolverDetalle" CssClass="btn btn-customback mb-4" runat="server" Text="Terminar y volver" OnClick="bVolverDetalle_Click" />
                </div>

            </div>

        </asp:Panel>

        <asp:Panel ID="pAbmAdmin" runat="server" Height="100%" Visible="false" Font-Size="Large" Font-Bold="true" BorderColor="Black" BorderStyle="Groove" CssClass="background-panel" ForeColor="WhiteSmoke">
            <div class="container mt-4">
                <h2 class="mb-4">Gestión de Administradores</h2>

                <asp:Label ID="lErrorAdministradores" runat="server" CssClass="text-danger" Visible="False"></asp:Label>

                <asp:Panel ID="pAgregarEditar" runat="server" CssClass="card p-4 mb-4" Visible="False">
                    <h3 class="card-title">Agregar/Editar Administrador</h3>

                    <div class="form-group">
                        <asp:Label ID="lblNombre" runat="server" Text="Nombre" CssClass="form-label"></asp:Label>
                        <asp:TextBox ID="tNombre" runat="server" CssClass="form-control" required></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <asp:Label ID="lblApellido" runat="server" Text="Apellido" CssClass="form-label"></asp:Label>
                        <asp:TextBox ID="tApellido" runat="server" CssClass="form-control" required></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <asp:Label ID="lblUsuario" runat="server" Text="Usuario" CssClass="form-label"></asp:Label>
                        <asp:TextBox ID="tUsuarioA" runat="server" CssClass="form-control" required></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <asp:Label ID="lblClave" runat="server" Text="Pass" CssClass="form-label"></asp:Label>
                        <asp:TextBox ID="tClaveA" runat="server" TextMode="Password" CssClass="form-control" required></asp:TextBox>
                    </div>

                    <div class="form-group">
                        <asp:Label ID="lblEmail" runat="server" Text="Email" CssClass="form-label"></asp:Label>
                        <asp:TextBox ID="tEmail" runat="server" CssClass="form-control" type="email" required></asp:TextBox>
                    </div>

                    <div class="div_login">
                        <asp:Button ID="bGuardar" runat="server" Text="Guardar" OnClick="bGuardar_Click" CssClass="btn btn-primary" />
                        <asp:Button ID="bCancelar" runat="server" Text="Cancelar" OnClick="bCancelar_Click" CssClass="btn btn-secondary" UseSubmitBehavior="False" />
                    </div>



                </asp:Panel>

                <asp:Panel ID="pLista" runat="server" CssClass="card p-4">
                    <h3 class="card-title">Lista de Administradores</h3>

                    <div class="table-responsive">
                        <asp:GridView ID="gAdministradores" runat="server" CssClass="table-abmadmin table table-striped" AutoGenerateColumns="False" DataKeyNames="IdAdmin" OnRowCommand="gAdministradores_RowCommand">
                            <Columns>
                                <asp:BoundField DataField="IdAdmin" HeaderText="ID" />
                                <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                                <asp:BoundField DataField="Apellido" HeaderText="Apellido" />
                                <asp:BoundField DataField="Usuario" HeaderText="Usuario" />
                                <asp:BoundField DataField="Email" HeaderText="Email" />
                                <asp:ButtonField CommandName="Editar" Text="Editar" ButtonType="Button" ControlStyle-CssClass="btn btn-warning btn-sm" />
                                <asp:ButtonField CommandName="Eliminar" Text="Eliminar" ButtonType="Button" ControlStyle-CssClass="btn btn-danger btn-sm" />
                            </Columns>
                        </asp:GridView>
                    </div>
                    <asp:Button ID="bNuevo" runat="server" Text="Nuevo Administrador" OnClick="bNuevo_Click" CssClass="btn btn-success mt-3" />
                    <asp:Button ID="bCancelVolverABMAdmin" runat="server" Text="Cancelar y Volver" CssClass="btn btn-secondary mt-4" OnClick="bCancelVolverABMAdmin_Click" />

                </asp:Panel>
            </div>


        </asp:Panel>



        <asp:Panel ID="pAbmCamisetas" runat="server" Height="100%" Visible="false" Font-Size="Large" Font-Bold="true" BorderColor="Black" BorderStyle="Groove" CssClass="background-panel">


            <asp:Panel ID="pAgregarEditarCami" runat="server" Visible="false" CssClass="card p-4  mt-4">
                <h3 class="card-title">Agregar/Editar Camiseta</h3>

                <div class="form-group mt-2">
                    <asp:Label ID="lEquipo" runat="server" Text="Equipo:" CssClass="form-label" />
                    <asp:TextBox ID="tEquipo" runat="server" MaxLength="50" CssClass="form-control" />
                </div>
                <div class="form-group">
                    <asp:Label ID="lPais" runat="server" Text="País:" CssClass="form-label" />
                    <asp:DropDownList ID="ddlPaises" runat="server" CssClass="form-select"></asp:DropDownList>
                </div>
                <div class="div_login">
                    <asp:Button ID="bGuardarAbmCami" runat="server" Text="Guardar" OnClick="bGuardarAbmCami_Click" CssClass="btn btn-primary mt-2" />
                    <asp:Button ID="bCancelarCami" runat="server" Text="Cancelar" CssClass="btn btn-secondary mt-2" OnClick="bCancelarCami_Click" />

                </div>
                <div class="form-group mt-2">
                    <asp:Label ID="lErrorCamisetas" runat="server" Visible="False" ForeColor="Red" CssClass="form-text text-danger" />
                </div>
            </asp:Panel>


            <asp:Panel ID="pListaCami" runat="server" CssClass=" card p-4 mt-4">
                <h3 class="card-title">Lista de Camisetas</h3>

                <div class="table-responsive">
                    <asp:GridView ID="gCamisetas" runat="server" AutoGenerateColumns="False" OnRowCommand="gCamisetas_RowCommand" CssClass="table table-striped">
                        <Columns>
                            <asp:BoundField DataField="codc" HeaderText="Código" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center" />
                            <asp:BoundField DataField="equipo" HeaderText="Equipo" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center" />
                            <asp:BoundField DataField="Pais" HeaderText="País" HeaderStyle-CssClass="text-center" ItemStyle-CssClass="text-center" />
                            <asp:ButtonField CommandName="Editar" Text="Editar" ButtonType="Button" ItemStyle-CssClass="text-center" ControlStyle-CssClass="btn btn-warning btn-sm" />
                            <asp:ButtonField CommandName="Eliminar" Text="Eliminar" ButtonType="Button" ItemStyle-CssClass="text-center" ControlStyle-CssClass="btn btn-danger btn-sm" />
                        </Columns>
                    </asp:GridView>
                </div>
                <div class="mb-3">
                    <asp:Button ID="bAgregar" runat="server" Text="Agregar Camiseta" Width="100%" OnClick="bAgregar_Click" CssClass="btn btn-success" />
                </div>
                <div class="mb-3">
                    <asp:Button ID="bCancelarVolverABMCami" runat="server" Text="Cancelar y Volver" Width="100%" CssClass="btn btn-secondary" OnClick="bCancelarVolverABMCami_Click" />
                </div>
            </asp:Panel>

        </asp:Panel>


        <asp:Panel ID="pUserActions" Height="100%" Visible="false" runat="server" CssClass="background-panel text-white p-4">
            <h2>Eliminar/Desactivar Usuario</h2>
            <div class="mb-3 mt-2">
                <asp:Label ID="lUsername" runat="server" Text="Usuario: " CssClass="form-label" />
                <asp:TextBox ID="tUsername" runat="server" MaxLength="50" CssClass="form-control" />
            </div>

            <div class="mb-3">
                <asp:Label ID="lRazonEliminacion" runat="server" Text="Razón de Eliminación: " CssClass="form-label" />
                <asp:TextBox ID="tRazonEliminacion" runat="server" MaxLength="200" TextMode="MultiLine" Rows="3" CssClass="form-control" />
            </div>

            <div class="d-grid gap-2 d-md-flex justify-content-md-end">
                <asp:Button ID="bEliminarUsuario" runat="server" CssClass="btn btn-danger me-2" Text="Eliminar Usuario" OnClick="bEliminarUsuario_Click" />
                <asp:Button ID="bDesactivarUsuario" runat="server" CssClass="btn btn-warning me-2" Text="Desactivar Usuario" OnClick="bDesactivarUsuario_Click" />
                <asp:Button ID="bCanVolverDesUsu" runat="server" CssClass="btn btn-secondary" Text="Cancelar y Volver" OnClick="bCanVolverDesUsu_Click" />
            </div>

            <asp:Label ID="lResultadoAccion" runat="server" Visible="False" ForeColor="Red" CssClass="mt-5" />
        </asp:Panel>


        <asp:Panel ID="pVerificaMail" Height="100%" Visible="false" runat="server" CssClass="background-panel text-white p-4">
            <div class="campo_centro">
                <p>Te enviamos un codigo de verificacion al email que ingresaste. abri el mail,</p>
                <p>copia el codigo, y pegalo en el cuadro de texto a continuacion.</p>
                <p>Luego oprimi Validar. Para corregir algun dato ingresado, oprimi</p>
                <p>"Volver al Registro"; o bien "Cancelar Y Volver" para anular la operacion y volver al login</p>

                <asp:TextBox ID="tValidar" runat="server" MaxLength="4" CssClass="form-control" Width="20%"/> 
                <asp:Label ID="lCodigo" runat="server" Visible="False" ForeColor="Orange" Text="*** CODIGO INCORRECTO ***" CssClass="mt-2" />
                <asp:Button ID="bValidar" type="button" CssClass="btn btn-customsucc mt-2" runat="server" Text="Validar El Codigo" Width="40%" OnClick="bValidar_Click" />
                <asp:Button ID="bRegresarRegistro" CssClass="btn btn-customAU mt-2 " runat="server" Text="Regresar Al Registro" Width="40%" OnClick="bRegresarRegistro_Click" />
                <asp:Button ID="bCancelarValidar" CssClass="btn btn-customback mt-4" runat="server" Text="Cancelar y volver" OnClick="bRegresarRegistro_Click" />
                <asp:Label ID="lErrorresU" runat="server" Visible="False" ForeColor="Red" Text="LerrorresU" CssClass="mt-2" />

            </div>
        </asp:Panel>



    </form>
</body>

</html>
