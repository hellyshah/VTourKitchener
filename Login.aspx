<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Header.Master" CodeBehind="Login.aspx.cs" Inherits="VTourKitchener.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="padding100">
        <div class="container">
            <div id="loginbox" style="margin-top: 50px;" class="mainbox col-md-6 col-md-offset-3 col-sm-8 col-sm-offset-2">
                <div class="panel panel-default">
                    <div class="panel-heading panel-heading-custom">
                        <div class="panel-title">
                            Sign In
                        </div>
                    </div>
                    <div style="padding-top: 30px" class="panel-body">
                        <div style="display: none" id="login-alert" class="alert alert-danger col-sm-12">
                        </div>
                        <div id="loginform" class="form-horizontal" role="form">
                            <div style="margin-bottom: 25px" class="input-group">
                                <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                                <asp:TextBox runat="server" ID="txtUserName" CssClass="form-control" placeholder="username or email"></asp:TextBox>
                            </div>
                            <div style="margin-bottom: 25px" class="input-group">
                                <span class="input-group-addon"><i class="glyphicon glyphicon-lock"></i></span>
                                <asp:TextBox runat="server" ID="txtPassword" CssClass="form-control" placeholder="password"
                                    TextMode="Password"></asp:TextBox>
                            </div>
                            <div class="input-group">
                                <div class="checkbox">
                                    <asp:Label ID="lblLoginValidaton" runat="server" Text=""></asp:Label>
                                </div>
                            </div>
                            <div style="margin-top: 10px" class="form-group">
                                <!-- Button -->
                                <div class="col-sm-12 controls">
                                    <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn btn-success" OnClick="btnLogin_Click" />
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-12 control">
                                    <div style="padding-top: 15px; font-size: 85%">
                                        Don't have an account! <a href="#" onclick="$('#loginbox').hide(); $('#signupbox').show()">Sign Up Here </a>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div id="signupbox" style="display: none; margin-top: 50px" class="mainbox col-md-6 col-md-offset-3 col-sm-8 col-sm-offset-2">
                <div class="panel panel-default">
                    <div class="panel-heading panel-heading-custom">
                        <div class="panel-title">
                            Sign Up
                        </div>
                        <div style="float: right; font-size: 85%; position: relative; top: -10px">
                        </div>
                    </div>
                    <div class="panel-body">
                        <div id="signupform" class="form-horizontal" role="form">
                            <div class="form-group">
                                <label for="email" class="col-md-3 control-label">
                                    Email</label>
                                <div class="col-md-9">
                                    <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control" placeholder="Email Address*"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="firstname" class="col-md-3 control-label">
                                    User Name</label>
                                <div class="col-md-9">
                                    <asp:TextBox runat="server" ID="txtUserNameSignUp" CssClass="form-control" placeholder="User Name*"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="lastname" class="col-md-3 control-label">
                                    Mobile</label>
                                <div class="col-md-9">
                                    <asp:TextBox runat="server" ID="txtMobile" CssClass="form-control" placeholder="Mobile Number*"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group">
                                <label for="password" class="col-md-3 control-label">
                                    Password</label>
                                <div class="col-md-9">
                                    <asp:TextBox runat="server" ID="txtPassWordtoReg" CssClass="form-control" placeholder="Password*" TextMode="Password"></asp:TextBox>
                                </div>
                            </div>
                            <div class="input-group">
                                <div class="checkbox">
                                    <asp:Label ID="lblValidationReg" runat="server" Text=""></asp:Label>
                                </div>
                            </div>
                            <div class="form-group">
                                <!-- Button -->
                                <div class="col-md-offset-3 col-md-9">
                                    <asp:Button ID="btnSignUp" runat="server" Text="Sign Up" CssClass="btn btn-success" OnClick="btnSignUp_Click" />
                                    <%--<button id="btn-signup" type="button" class="btn btn-info">
                                        <i class="icon-hand-right"></i>&nbsp </button>--%>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
