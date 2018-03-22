<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ASGuf2.NewLogin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-type" content="text/html; charset=utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=Edge">
    <title>Информационный ресурс Департамента</title>
    <link rel="stylesheet" href="Content/login.css" type="text/css">
    <script type="text/javascript" src="/Scripts/jquery-1.11.1.min.js"></script>
    <script type="text/javascript" src="/Scripts/notify.min.js"></script>
    <script type="text/javascript" src="/Scripts/jquery-migrate-1.2.1.min.js"></script>
    <link rel="shortcut icon" href="Images/ico.ico" />
</head>
<body>
    <form id="form1" runat="server">
    <!-- <div class="bg"></div> -->
    <h1>Информационный ресурс Департамента</h1>
    <div class="logo_box">
        <img class="logo" src="Images/logo1.png" alt=""/>
        <ul class="aside-menu">
            <li>ДЕПАРТАМЕНТ</li>
            <li>ГОРОДСКОГО ИМУЩЕСТВА</li>
            <li>ГОРОДА МОСКВЫ</li>
        </ul>
        <div id="loginBox" class="rounded">
            <img class="icon" src="Images/Username.png" alt=""/>
            <input type="text" id="login_box" name="q" placeholder="Логин"/>
        </div>
        <div id="passBox" class="rounded" style="margin-top: 20px">
            <img class="icon2" src="Images/Password.png" alt=""/>
            <input type="password" id="password_box" name="q" placeholder="Пароль"/>
        </div>
        <button id="bbt" class="btn"> Вход </button>
        </p>
        <div class="info_box">          
            <a href="mailto:DGI-Support@mos.ru">Поддержка</a>
        </div>
    </div>
    <div class="modal">
        <div class="windows8">
            <div class="wBall" id="wBall_1">
                <div class="wInnerBall"></div>
            </div>
            <div class="wBall" id="wBall_2">
                <div class="wInnerBall"></div>
            </div>
            <div class="wBall" id="wBall_3">
                <div class="wInnerBall"></div>
            </div>
            <div class="wBall" id="wBall_4">
                <div class="wInnerBall"></div>
            </div>
            <div class="wBall" id="wBall_5">
                <div class="wInnerBall"></div>
            </div>
        </div>
    </div>
</form>
</body>
<script type="text/javascript" language="javascript">
$(document).ready(function() {
    $body = $("body");

    // Показать анимацию загрузки

    $.notify.addStyle('Natificate', {
        html: "<div style='border: 1px solid rgba(255, 100, 89, 0.2); border-radius: 20px;'>" +
            "<img src='Images/esrd__0001_warning-copy-2.png' alt='' style='position:absolute; left:12px; bottom:8px; width:6px;height: 20px;'>" +
            "<span style='margin-left:12px;' data-notify-text/>" +
            "</div>",
        classes: {
            base: {
                "white-space": "nowrap",
                "background-color": "rgba(0, 0, 0, 0.4)",
                "padding": "6px 10px",
                "color": "rgba(255, 100, 89, 0.7)",
                "font-family": "CaviarDreamsRegular",
                "font-size": "18px",
            }
        }
    });

    $("#bbt").click(function(e) {
        e.preventDefault();
        var login = $('#login_box').val();
        var pass = $('#password_box').val();
        if (!login) {
            $("#loginBox").notify("Необходим логин", {
                position: "left",
                autoHide: true,
                autoHideDelay: 8000,
                style: 'Natificate',
                arrowShow: false,
                hideDuration: 400,
                showDuration: 400
            });
        } else if (!pass) {
            $("#passBox").notify("Необходим пароль", {
                position: "left",
                autoHide: true,
                autoHideDelay: 8000,
                style: 'Natificate',
                arrowShow: false,
                hideDuration: 400,
                showDuration: 400
            });
        } else {
            var data = {
                Username: login,
                Password: pass
            };
            $body.addClass("loading");
            $.ajax({
                type: "POST",
                url: "Login.aspx/Authentification",
                data: JSON.stringify({logindata: data}),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    if (response.d == "")
                    {
                        if (document.location.hostname == "localhost")
                            window.location.replace("/Default.aspx");
                        else
                            window.location.replace("/esrd/Default.aspx");
                    }
                    else
                    {         
                        $("#loginBox").notify(response.d, {
                        position: "top center",
                        autoHide: true,
                        autoHideDelay: 8000,
                        style: 'Natificate',
                        arrowShow: false,
                        hideDuration: 400,
                        showDuration: 400
                        });
                        $body.removeClass("loading");
                    }
                }
            });



        }

    });
});

// if ($.browser.webkit) {
//     $('input[name="password"]').attr('autocomplete', 'off');
// }
</script>

</html>
