<%@ Page Title="Информационный ресурс" Language="C#" MasterPageFile="~/Site.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApplication1._Default" %>



<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="jumbotron">    

        <div class="navpanel" runat="server" id="navpanel" style="margin: auto; margin-bottom: 5px">
        <table style="width: 100%; border-spacing: 0px; te">
            <tr>
                <td style="width: 300px; border-style:solid; border-width:1px; border-color: midnightblue; vertical-align: top;">
                      <p style="text-align: center">Активные фильтры:</p>
            <asp:Textbox ID="Labfiltr" runat="server" style="overflow:hidden; resize:none" Width="300px" BorderStyle="None" ReadOnly="true" TextMode="MultiLine" Height="186px">Нет активных фильтров</asp:Textbox>

                </td>
                <td style="border-style:solid; border-width:1px; border-color: midnightblue; border-right:none;border-left:none "> <asp:Label ID="Label3" runat="server" Height="30px" Text="Введите номера логик через зарятую:"></asp:Label>
            <asp:Label ID="Label1" runat="server" Height="30px" Text="Номер или название логики:"></asp:Label>
            &nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="TextBox1" runat="server" Height="30px" Width="300px" Style="font-size: small" TabIndex="1"></asp:TextBox>
            <span style="font-size: small">&nbsp;&nbsp;
            &nbsp;&nbsp;&nbsp;
            <asp:CheckBox ID="CheckBox1" runat="server" Text=" Поиск по нескольким логикам" Font-Bold="False" AutoPostBack="True" />
                &nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="Button1" runat="server" Height="37px" Text="Поиск" Style="font-size: small" Width="70px" OnClick="Button1_Click" TabIndex="2" />&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="Button2" runat="server" Height="37px" Text="Сброс" Style="font-size: small" Width="70px" OnClick="Button2_Click" TabIndex="3" />                
                    <br />
                    <br />
            </span>
            <asp:Button ID="Button4" runat="server" Height="37px" Text="Добавить" Style="font-size: small" OnClick="Button4_Click" TabIndex="4" />

            &nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="Button7" runat="server" Height="37px" OnClick="Button7_Click" Text="Выгрузить" TabIndex="5" /></td>
                <td style="width: 300px; vertical-align: top; border-style:solid; border-width:1px; border-color: midnightblue; "><p style="text-align: center">Легенда</p>
                <p style="text-align: left">
                    <asp:Image runat="server" ImageUrl="~/Images/creating.jpg" Height="20px" />
                    &nbsp;В процессе реализации
                </p>
                <p style="text-align: left">
                    <asp:Image runat="server" ImageUrl="~/Images/notaccepted.jpg" Height="20px" />
                    &nbsp;Не принято на реализацию
                </p>
                <p style="text-align: left">
                    <asp:Image runat="server" ImageUrl="~/Images/intime.jpg" Height="20px" />
                    &nbsp;Выполнено в срок
                </p>
                <p style="text-align: left">
                    <asp:Image runat="server" ImageUrl="~/Images/withoverdue.jpg" Height="20px" />
                    &nbsp;Выполнено с нарушением срока
                </p>
                <p style="text-align: left">
                    <asp:Image runat="server" ImageUrl="~/Images/overdue.jpg" Height="20px" />
                    &nbsp;Превышен срок реализации
                </p></td>
            </tr>
        </table></div>


        <div class="text-center">       
          
            <div class="popup" runat="server" id="popupdiv" style="margin: auto; padding: 10px; width: 900px; border-radius: 1px; border-style: solid; border-color: #006699; border-width: 1px; background-color: #fff">
                <asp:Panel ID="popupPanel" VerticalAlign="Center" runat="server">
                    &nbsp;<table style="width: 100%">
                        <tr>
                            <td>&nbsp;</td>
                            <td class="card-label" style="width: 321px">
                                <asp:Label ID="Label4" runat="server" Text="Название процесса в СПД:" TextAlign="center" Width="310px"></asp:Label>
                            </td>
                            <td class="card-textbox">
                                <asp:TextBox ID="TextBox2" runat="server" Font-Underline="False" Height="100" Style="resize: none" TextMode="MultiLine" Width="400px" Wrap="true" TabIndex="6"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td class="card-label" style="width: 321px">
                                <asp:Label ID="Label2" runat="server" Text="Номер процесса в СПД:" Width="310px"></asp:Label>
                            </td>
                            <td class="card-textbox">
                                <asp:TextBox ID="TextBox3" runat="server" Font-Underline="False" Height="20px" Style="resize: none" Width="400" Wrap="true" TabIndex="7"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td class="card-label" style="width: 321px">
                                <asp:Label ID="Label5" runat="server" Text="Рубрика в ЕСРД/ДК:" Width="310px"></asp:Label>
                            </td>
                            <td class="card-textbox">
                                <asp:TextBox ID="TextBox4" runat="server" Font-Underline="False" Height="20px" Style="resize: none" Width="400" Wrap="true" TabIndex="8"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td class="card-label" style="width: 321px">
                                <asp:Label ID="Label6" runat="server" Text="ID DK:" Width="310px"></asp:Label>
                            </td>
                            <td class="card-textbox">
                                <asp:TextBox ID="TextBox5" runat="server" Font-Underline="False" Height="20px" Style="resize: none" Width="400" Wrap="true" TabIndex="9"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td class="card-label" style="width: 321px">
                                <asp:Label ID="Label7" runat="server" Text="Дата получения логики:" Width="310px"></asp:Label>
                            </td>
                            <td class="card-textbox">
                                <asp:TextBox ID="TextBox6" runat="server" Font-Underline="False" Height="20px" Width="400px" Wrap="true" TextMode="Date" TabIndex="10"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td class="card-label" style="width: 321px">
                                <asp:Label ID="Label8" runat="server" Text="Номер служебной записки, по которой логика направлялась:" Width="310px"></asp:Label>
                            </td>
                            <td class="card-textbox">
                                <asp:TextBox ID="TextBox7" runat="server" Font-Underline="False" Height="20px" Style="resize: none" Width="400" Wrap="true" TabIndex="11"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td class="card-label" style="width: 321px">
                                <asp:Label ID="Label9" runat="server" Text="Реакция Управления информатизации:" Width="310px"></asp:Label>
                            </td>
                            <td class="card-textbox">
                                <asp:DropDownList ID="DropDownList1" runat="server" Height="20px" Width="400px" TabIndex="12">
                                    <asp:ListItem>Логика принята</asp:ListItem>
                                    <asp:ListItem>Логика не принята</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>

                        <tr>
                            <td>&nbsp;</td>
                            <td class="card-label" style="width: 321px">
                                <asp:Label ID="Label27" runat="server" Text="Тип логики процесса:" Width="310px"></asp:Label>
                            </td>
                            <td class="card-textbox">
                                <asp:DropDownList ID="DropDownList2" runat="server" Height="20px" Width="400px" TabIndex="12">
                                    <asp:ListItem>Актуализация</asp:ListItem>
                                    <asp:ListItem>Реализация</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>

                        <tr>
                            <td>&nbsp;</td>
                            <td class="card-label" style="width: 321px">
                                <asp:Label ID="Label11" runat="server" Text="Дата ответной служебной записки Управления информатизации:" Width="310px"></asp:Label>
                            </td>
                            <td class="card-textbox">
                                <asp:TextBox ID="TextBox9" runat="server" Font-Underline="False" Height="20px" Style="resize: none" Width="400" Wrap="true" TextMode="Date" TabIndex="13"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td class="card-label" style="width: 321px">
                                <asp:Label ID="Label10" runat="server" Text="Номер ответной служебной записки Управления информатизации:" Width="310px"></asp:Label>
                            </td>
                            <td class="card-textbox">
                                <asp:TextBox ID="TextBox10" runat="server" Font-Underline="False" Height="20px" Style="resize: none" Width="400" Wrap="true" TabIndex="14"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td class="card-label" style="width: 321px">
                                <asp:Label ID="Label13" runat="server" Text="Номер GLPI:" Width="310px"></asp:Label>
                            </td>
                            <td class="card-textbox">
                                <asp:TextBox ID="TextBox12" runat="server" Font-Underline="False" Height="20px" Style="resize: none" Width="400" Wrap="true" TabIndex="15"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td class="card-label" style="width: 321px">
                                <asp:Label ID="Label14" runat="server" Text="Дата Акта о передаче на тестирование (дата реализации процесса):" Width="310px"></asp:Label>
                            </td>
                            <td class="card-textbox">
                                <asp:TextBox ID="TextBox13" runat="server" Font-Underline="False" Height="20px" Style="resize: none" Width="400" Wrap="true" TextMode="Date" TabIndex="16"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td class="card-label" style="width: 321px">
                                <asp:Label ID="Label15" runat="server" Text="Номер Акта о передаче на тестирование:" Width="310px"></asp:Label>
                            </td>
                            <td class="card-textbox">
                                <asp:TextBox ID="TextBox14" runat="server" Font-Underline="False" Height="20px" Style="resize: none" Width="400" Wrap="true" TabIndex="17"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td class="card-label" style="width: 321px">
                                <asp:Label ID="Label16" runat="server" Text="Дата Акта (служебной записки) о вводе в эксплуатацию:" Width="310px"></asp:Label>
                            </td>
                            <td class="card-textbox">
                                <asp:TextBox ID="TextBox15" runat="server" Font-Underline="False" Height="20px" Style="resize: none" Width="400" Wrap="true" TextMode="Date" TabIndex="18"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td class="card-label" style="width: 321px">
                                <asp:Label ID="Label17" runat="server" Text="Номер Акта (служебной записки) о вводе в эксплуатацию:" Width="310px"></asp:Label>
                            </td>
                            <td class="card-textbox">
                                <asp:TextBox ID="TextBox16" runat="server" Font-Underline="False" Height="20px" Style="resize: none" Width="400" Wrap="true" TabIndex="19"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td class="card-label" style="width: 321px">
                                <asp:Label ID="Label18" runat="server" Height="16px" Text="Комментарий:" Width="310px"></asp:Label>
                            </td>
                            <td class="card-textbox">
                                <asp:TextBox ID="TextBox17" runat="server" Font-Underline="False" Height="100" Style="resize: none" TextMode="MultiLine" Width="400" Wrap="true" TabIndex="20"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td class="card-label" style="width: 321px">
                                <asp:Label ID="Label19" runat="server" Text="Ссылка на БР:" Width="310px"></asp:Label>
                            </td>
                            <td class="card-textbox">
                                <asp:TextBox ID="TextBox18" runat="server" Font-Underline="False" Height="20px" Style="resize: none" Width="400" Wrap="true" TabIndex="22"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td class="card-label" style="width: 321px">
                                <asp:Label ID="Label20" runat="server" Text="Управление-исполнитель по логике:" Width="310px"></asp:Label>
                            </td>
                            <td class="card-textbox">
                                <asp:TextBox ID="TextBox19" runat="server" Font-Underline="False" Height="20px" Style="resize: none" Width="400" Wrap="true" TabIndex="23"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td class="card-label" style="width: 321px">
                                <asp:Label ID="Label22" runat="server" Text="Плановая дата:" Width="310px"></asp:Label>
                            </td>
                            <td class="card-textbox">
                                <asp:TextBox ID="TextBox21" runat="server" Font-Underline="False" Height="20px" Style="resize: none" Width="400" TextMode="Date" TabIndex="24"></asp:TextBox>
                                <asp:CheckBox ID="CheckBox2" runat="server" Checked="True" Text="Рассчитать" />
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td class="card-label" style="width: 321px">
                                <asp:Label ID="Label23" runat="server" Text="Время, завтра- ченное на реализацию процесса (передачу на тестирование):" Width="310px"></asp:Label>
                            </td>
                            <td class="card-textbox">
                                <asp:TextBox ID="TextBox30" runat="server" Font-Underline="False" Height="20px" Style="resize: none" Width="400" Wrap="true" TabIndex="23" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td class="card-label" style="width: 321px">
                                <asp:Label ID="Label24" runat="server" Text="Статус реализации (Норматив - 14 рабочих дней):" Width="310px"></asp:Label>
                            </td>
                            <td class="card-textbox">
                                <asp:TextBox ID="TextBox31" runat="server" Font-Underline="False" Height="20px" Style="resize: none" Width="400" Wrap="true" TabIndex="23" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td class="card-label" style="width: 321px">
                                <asp:Label ID="Label25" runat="server" Text="Время, затра- ченное на ввод в эксплуатацию после тестирования (реализации) процесса:" Width="310px"></asp:Label>
                            </td>
                            <td class="card-textbox">
                                <asp:TextBox ID="TextBox32" runat="server" Font-Underline="False" Height="20px" Style="resize: none" Width="400" Wrap="true" TabIndex="23" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td class="card-label" style="width: 321px">
                                <asp:Label ID="Label26" runat="server" Text="Общее время от передачи на реализацию Логики до ввода в эксплуатацию" Width="310px"></asp:Label>
                            </td>
                            <td class="card-textbox">
                                <asp:TextBox ID="TextBox33" runat="server" Font-Underline="False" Height="20px" Style="resize: none" Width="400" Wrap="true" TabIndex="23" Enabled="false"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <asp:Button ID="Button3" runat="server" Height="40" OnClick="Button3_Click" Text="Добавить" ValidationGroup="txt" TabIndex="25" />&nbsp;&nbsp;&nbsp;
                  <asp:Button ID="Button5" runat="server" Height="40px" OnClick="Button5_Click" Text="Внести изменения" ValidationGroup="txt" TabIndex="25" />
                    &nbsp;&nbsp;&nbsp;
                  <asp:Button ID="Button6" runat="server" Height="40px" OnClick="Button2_Click" Text="Отмена" Width="70px" TabIndex="26" />
                    &nbsp;&nbsp;
                </asp:Panel>
                <div class="FormError" runat="server" id="Div2">
                    <asp:Label ID="Err" runat="server" Text="" Width="410px"></asp:Label>
                </div>
            </div>            
            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="Black" BorderStyle="None" CellPadding="3" BorderWidth="2px" HorizontalAlign="Center" AllowPaging="True" PageSize="20" OnPageIndexChanging="GridView2_PageIndexChanging" OnRowDeleting="GridView2_RowDeleting" OnRowEditing="GridView2_RowEditing" OnRowDataBound="GridView2_RowDataBound" PagerStyle-Wrap="False" CssClass="grid">

                <Columns>

                    <asp:TemplateField HeaderText="Название процесса в СПД" SortExpression="A">
                        <HeaderTemplate>
                            <table class="table1" style="width: 100%; /* ширина таблицы */
                        height: 255px">
                                <tr>
                                    <td style="vertical-align: top;">Фильтры:&nbsp;&nbsp;
                                        <asp:TextBox ID="TextBox23" BackColor="#556b7f" ForeColor="White" runat="server" Width="105px" BorderColor="Black" BorderStyle="Inset" BorderWidth="1px" TabIndex="28"></asp:TextBox><br />
                                        <hr />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top;">Название процесса в СПД   
                                    </td>
                                </tr>
                            </table>

                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("LOGNAME") %>'></asp:Label>
                        </ItemTemplate>
                        <ControlStyle Width="200px" />
                        <HeaderStyle Width="250px" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Номер про- цесса в СПД" SortExpression="B">
                        <HeaderTemplate>
                            <table class="table1" style="width: 100%; /* ширина таблицы */
                        height: 255px">
                                <tr>
                                    <td style="vertical-align: top;">
                                        <asp:TextBox ID="TextBox24" runat="server" BorderColor="Black" BorderStyle="Inset" BorderWidth="1px" BackColor="#556b7f" ForeColor="White" Width="30px" TabIndex="27"></asp:TextBox><br />
                                        <hr />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top;">Номер про- цесса в СПД</td>
                                </tr>
                            </table>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label3" runat="server" Text='<%# Bind("RUBRNAMESPD") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Руб- рика в ЕСРД/ДК" SortExpression="C">
                        <ItemTemplate>
                            <asp:Label ID="Label4" runat="server" Text='<%# Bind("RUBRNAMEESRD") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderTemplate>
                            <table class="table1" style="width: 100%; height: 255px">
                                <tr>
                                    <td style="vertical-align: top;">
                                        <asp:TextBox ID="TextBox25" runat="server" BackColor="#556b7f" BorderColor="Black" BorderStyle="Inset" BorderWidth="1px" ForeColor="White" Width="30px" TabIndex="27"></asp:TextBox><br />
                                        <hr />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top;">Руб- рика в ЕСРД/ДК  </td>
                                </tr>
                            </table>
                        </HeaderTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="ID DK" SortExpression="D">
                        <ItemTemplate>
                            <asp:Label ID="Label5" runat="server" Text='<%# Bind("IDDK") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderTemplate>
                            <table class="table1" style="width: 100%; height: 255px">
                                <tr>
                                    <td style="vertical-align: top;">
                                        <asp:TextBox ID="TextBox26" runat="server" BackColor="#556b7f" BorderColor="Black" BorderStyle="Inset" BorderWidth="1px" ForeColor="White" Width="30px" TabIndex="27"></asp:TextBox><br />
                                        <hr />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top;">ID DK</td>
                                </tr>
                            </table>
                        </HeaderTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Дата получения логики" SortExpression="E">
                        <ItemTemplate>
                            <asp:Label ID="Label6" runat="server" Text='<%# Bind("DATEAR") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderTemplate>
                            <table class="table1" style="width: 100%; height: 255px">
                                <tr>
                                    <td style="vertical-align: top;">
                                        <asp:TextBox ID="TextBox27" runat="server" BorderColor="Black" BorderStyle="Inset" BorderWidth="1px" BackColor="#556b7f" ForeColor="White" Width="35px" TabIndex="27" TextMode="Date"></asp:TextBox><br />
                                        <hr />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top;">Дата получения логики</td>
                                </tr>
                            </table>
                        </HeaderTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Номер служебной записки, по которой логика направ- лялась" SortExpression="F">
                        <HeaderTemplate>
                            <table class="table1" style="width: 100%; height: 255px">
                                <tr>
                                    <td style="vertical-align: top;">
                                        <asp:TextBox ID="TextBox28" BorderColor="Black" BorderStyle="Inset" BorderWidth="1px" runat="server" BackColor="#556b7f" ForeColor="White" Width="35px" TabIndex="27"></asp:TextBox><br />
                                        <hr />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top;">Номер служебной записки, по которой логика направ- лялась</td>
                                </tr>
                            </table>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl='<%# Eval("DOCNUM", "http://dgi-glpi.mlc.gov/glpi/front/ticket.form.php?id={0}") %>' Target="_blank" Text='<%# Eval("DOCNUM") %>'></asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Реакция Управления информа- тизации" SortExpression="G">
                        <ItemTemplate>
                            <asp:Label ID="Label8" runat="server" Text='<%# Bind("REACT") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderTemplate>
                            <table class="table1" style="width: 100%; height: 255px">
                                <tr>
                                    <td style="vertical-align: top;">
                                        <asp:DropDownList ID="DRL1" BorderColor="Black" BorderStyle="Inset" BorderWidth="1px" BackColor="#556b7f" ForeColor="White" runat="server" Width="20px" TabIndex="27">
                                            <asp:ListItem Text="---" Value=""></asp:ListItem>
                                            <asp:ListItem Text="Логика принята" Value="Логика принята"></asp:ListItem>
                                            <asp:ListItem Text="Логика не принята" Value="Логика не принята"></asp:ListItem>
                                        </asp:DropDownList><br />
                                        <hr />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top;">Реакция Управ- ления информа- тизации </td>
                                </tr>
                            </table>
                        </HeaderTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Тип логики процесса" SortExpression="G">
                        <ItemTemplate>
                            <asp:Label ID="Label34" runat="server" Text='<%# Bind("ISNEW") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderTemplate>
                            <table class="table1" style="width: 100%; height: 255px">
                                <tr>
                                    <td style="vertical-align: top;">
                                        <asp:DropDownList ID="DRL4" BorderColor="Black" BorderStyle="Inset" BorderWidth="1px" BackColor="#556b7f" ForeColor="White" runat="server" Width="20px" TabIndex="27">
                                            <asp:ListItem Text="---" Value=""></asp:ListItem>
                                            <asp:ListItem Text="Актуализация" Value="Актуализация"></asp:ListItem>
                                            <asp:ListItem Text="Реализация" Value="Реализация"></asp:ListItem>
                                        </asp:DropDownList><br />
                                        <hr />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top;">Тип логики процесса</td>
                                </tr>
                            </table>
                        </HeaderTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Дата ответной служебной записки Управления информа- тизации" SortExpression="I">
                        <ItemTemplate>
                            <asp:Label ID="Label9" runat="server" Text='<%# Bind("REACTDATE") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderTemplate>
                            <table class="table1" style="width: 100%; height: 255px">
                                <tr>
                                    <td style="vertical-align: top;">
                                        <asp:TextBox ID="TextBox29" runat="server" BorderColor="Black" BorderStyle="Inset" BorderWidth="1px" BackColor="#556b7f" ForeColor="White" Width="35px" TabIndex="27" TextMode="Date"></asp:TextBox><br />
                                        <hr />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top;">Дата ответной слу- жебной записки Управ- ления информа- тизации</td>
                                </tr>
                            </table>
                        </HeaderTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Номер ответной служебной записки Управления информа- тизации" SortExpression="H">
                        <ItemTemplate>
                            <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl='<%# Eval("DOCNUMREACT", "http://dgi-glpi.mlc.gov/glpi/front/ticket.form.php?id={0}") %>' Target="_blank" Text='<%# Eval("DOCNUMREACT") %>'></asp:HyperLink>
                        </ItemTemplate>
                        <HeaderTemplate>
                            <table class="table1" style="width: 100%; height: 255px">
                                <tr>
                                    <td style="vertical-align: top;">
                                        <asp:TextBox ID="TextBox30" runat="server" BorderColor="Black" BorderStyle="Inset" BorderWidth="1px" BackColor="#556b7f" ForeColor="White" Width="35px" TabIndex="27"></asp:TextBox><br />
                                        <hr />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top;">Номер ответной служебной записки Управления информа- тизации</td>
                                </tr>
                            </table>
                        </HeaderTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Номер GLPI" SortExpression="K">
                        <ItemTemplate>
                            <asp:HyperLink ID="HyperLink1" runat="server" Target="_blank" NavigateUrl='<%# Eval("GLPI", "http://dgi-glpi.mlc.gov/glpi/front/ticket.form.php?id={0}") %>' Text='<%# Eval("GLPI") %>'></asp:HyperLink>
                        </ItemTemplate>
                        <HeaderTemplate>
                            <table class="table1" style="width: 100%; height: 255px">
                                <tr>
                                    <td style="vertical-align: top;">
                                        <asp:TextBox ID="TextBox31" runat="server" BorderColor="Black" BorderStyle="Inset" BorderWidth="1px" BackColor="#556b7f" ForeColor="White" Width="30px" TabIndex="27"></asp:TextBox><br />
                                        <hr />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top;">Номер GLPI</td>
                                </tr>
                            </table>
                        </HeaderTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Дата Акта о передаче на тестиро- вание (дата реализа- ции процесса)" SortExpression="L">
                        <ItemTemplate>
                            <asp:Label ID="Label11" runat="server" Text='<%# Bind("TESTDATE") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderTemplate>
                            <table class="table1" style="width: 100%; height: 255px">
                                <tr>
                                    <td style="vertical-align: top;">
                                        <asp:TextBox ID="TextBox32" runat="server" BorderColor="Black" BorderStyle="Inset" BorderWidth="1px" BackColor="#556b7f" ForeColor="White" Width="35px" TabIndex="27" TextMode="Date"></asp:TextBox><br />
                                        <hr />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top;">Дата Акта о передаче на тестиро- вание (дата реализа- ции процесса)</td>
                                </tr>
                            </table>
                        </HeaderTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Номер Акта о передаче на тестирование" SortExpression="M">
                        <ItemTemplate>
                            <asp:HyperLink ID="HyperLink5" runat="server" NavigateUrl='<%# Eval("TESTDOC", "http://dgi-glpi.mlc.gov/glpi/front/ticket.form.php?id={0}") %>' Target="_blank" Text='<%# Eval("TESTDOC") %>'></asp:HyperLink>
                        </ItemTemplate>
                        <HeaderTemplate>
                            <table class="table1" style="width: 100%; height: 255px">
                                <tr>
                                    <td style="vertical-align: top;">
                                        <asp:TextBox ID="TextBox33" runat="server" BorderColor="Black" BorderStyle="Inset" BorderWidth="1px" BackColor="#556b7f" ForeColor="White" Width="35px" TabIndex="27"></asp:TextBox><br />
                                        <hr />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top;">Номер Акта о передаче на тестирование</td>
                                </tr>
                            </table>
                        </HeaderTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Дата Акта (слу- жебной записки) о вводе в эксплуат- ацию" SortExpression="N">
                        <ItemTemplate>
                            <asp:Label ID="Label13" runat="server" Text='<%# Bind("CREATEDATE") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderTemplate>
                            <table class="table1" style="width: 100%; height: 255px">
                                <tr>
                                    <td style="vertical-align: top;">
                                        <asp:TextBox ID="TextBox34" runat="server" BorderColor="Black" BorderStyle="Inset" BorderWidth="1px" BackColor="#556b7f" ForeColor="White" Width="35px" TabIndex="27" TextMode="Date"></asp:TextBox><br />
                                        <hr />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top;">Дата Акта (слу- жебной записки) о вводе в эксплуата- цию</td>
                                </tr>
                            </table>
                        </HeaderTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Номер Акта (служебной записки) о вводе в эксплуата- цию" SortExpression="O">
                        <ItemTemplate>
                            <asp:HyperLink ID="HyperLink6" runat="server" NavigateUrl='<%# Eval("CREATEDOC", "http://dgi-glpi.mlc.gov/glpi/front/ticket.form.php?id={0}") %>' Target="_blank" Text='<%# Eval("CREATEDOC") %>'></asp:HyperLink>
                        </ItemTemplate>
                        <HeaderTemplate>
                            <table class="table1" style="width: 100%; height: 255px">
                                <tr>
                                    <td style="vertical-align: top;">
                                        <asp:TextBox ID="TextBox35" BackColor="#556b7f" BorderColor="Black" BorderStyle="Inset" BorderWidth="1px" ForeColor="White" runat="server" Width="35px" TabIndex="27"></asp:TextBox><br />
                                        <hr />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top;">Номер Акта (служебной записки) о вводе в эксплуата- цию</td>
                                </tr>
                            </table>
                        </HeaderTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Коммен- тарий" SortExpression="P">
                        <ItemTemplate>
                            <asp:Label ID="Label15" runat="server" Text='<%# Bind("COMMENTR") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderTemplate>
                            <table class="table1" style="width: 100%; height: 255px">
                                <tr>
                                    <td style="vertical-align: top;">
                                        <asp:TextBox ID="TextBox36" runat="server" BorderColor="Black" BorderStyle="Inset" BorderWidth="1px" BackColor="#556b7f" ForeColor="White" Width="35px" TabIndex="27"></asp:TextBox><br />
                                        <hr />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top;">Коммен- тарий</td>
                                </tr>
                            </table>
                        </HeaderTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Наличие логики в БР" SortExpression="Q">

                        <HeaderTemplate>
                            <table class="table1" style="width: 100%; height: 255px">
                                <tr>
                                    <td style="vertical-align: top;">
                                        <asp:DropDownList ID="DRL2" runat="server" BorderColor="Black" BorderStyle="Inset" BorderWidth="1px" BackColor="#556b7f" ForeColor="White" Width="20px" TabIndex="27">
                                            <asp:ListItem Text="---" Value=""></asp:ListItem>
                                            <asp:ListItem Text="Да" Value="Есть"></asp:ListItem>
                                            <asp:ListItem Text="Нет" Value="Нет"></asp:ListItem>
                                        </asp:DropDownList><br />
                                        <hr />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top;">Наличие логики в БР</td>
                                </tr>
                            </table>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl='<%# Eval("ISINBR", "http://webreon2.mlc.gov/baseregweb2015/default1.aspx#/response?id_documentres={0}") %>' Target="_blank"></asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Управление - исполнитель по логике" SortExpression="R">
                        <ItemTemplate>
                            <asp:Label ID="Label17" runat="server" Text='<%# Bind("ISPUPR") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderTemplate>
                            <table class="table1" style="width: 100%; height: 255px">
                                <tr>
                                    <td style="vertical-align: top;">
                                        <asp:TextBox ID="TextBox38" runat="server" BackColor="#556b7f" ForeColor="White" BorderColor="Black" BorderStyle="Inset" BorderWidth="1px" Width="35px" TabIndex="27"></asp:TextBox><br />
                                        <hr />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top;">Управление - исполнитель по логике</td>
                                </tr>
                            </table>
                        </HeaderTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Плановая дата" SortExpression="T">
                        <ItemTemplate>
                            <asp:Label ID="Label18" runat="server" Text='<%# Bind("PLANDATE") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderTemplate>
                            <table class="table1" style="width: 100%; height: 255px">
                                <tr>
                                    <td style="vertical-align: top;">
                                        <asp:TextBox ID="TextBox39" runat="server" BorderColor="Black" BorderStyle="Inset" BorderWidth="1px" BackColor="#556b7f" ForeColor="White" Width="35px" TabIndex="27" TextMode="Date"></asp:TextBox><br />
                                        <hr />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top;">Плановая дата</td>
                                </tr>
                            </table>
                        </HeaderTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Время, завтра- ченное на реали- зацию процесса (передачу на тести- рование)">
                        <ItemTemplate>
                            <asp:Label ID="Label19" runat="server" Text='<%# Bind("daypasstest") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderTemplate>
                            <table class="table1" style="width: 100%; height: 255px">
                                <tr>
                                    <td style="vertical-align: top;">
                                        <asp:TextBox ID="TextBox40" runat="server" BorderColor="Black" BorderStyle="Inset" BorderWidth="1px" Width="30px" BackColor="#556b7f" ForeColor="White" Visible="False" TabIndex="27"></asp:TextBox><br />
                                        <hr />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top;">Время, завтра- ченное на реали- зацию процесса (передачу на тести- рование)</td>
                                </tr>
                            </table>
                        </HeaderTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Статус реализации (Норматив - 14 рабочих дней):" SortExpression="J">
                        <HeaderTemplate>
                            <table class="table1" style="width: 100%; height: 255px">
                                <tr>
                                    <td style="vertical-align: top;">
                                        <asp:DropDownList ID="DRL3" runat="server" Width="20px" BackColor="#556b7f" BorderColor="Black" BorderStyle="Inset" BorderWidth="1px" ForeColor="White" TabIndex="27">
                                            <asp:ListItem Text="---" Value=""></asp:ListItem>
                                            <asp:ListItem Text="Реализуется в срок" Value="Реализуется в срок"></asp:ListItem>
                                            <asp:ListItem Text="Выполнено в срок" Value="Выполнено в срок"></asp:ListItem>
                                            <asp:ListItem Text="Превышен срок реализации" Value="Превышен срок реализации"></asp:ListItem>
                                            <asp:ListItem Text="Выполнено с нарушением срока" Value="Выполнено с нарушением срока"></asp:ListItem>
                                        </asp:DropDownList><br />
                                        <hr />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top;">Статус реализации (Норматив - 14 рабочих дней):</td>
                                </tr>
                            </table>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label2" runat="server" Text='<%# Bind("TIMEPASS") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Время, затра- ченное на ввод в эксплуа- тацию после тести- рования (реали- зации) процесса">
                        <ItemTemplate>
                            <asp:Label ID="Label20" runat="server" Text='<%# Bind("createdaypass") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderTemplate>
                            <table class="table1" style="width: 100%; height: 255px">
                                <tr>
                                    <td style="vertical-align: top;">
                                        <asp:TextBox ID="TextBox41" runat="server" BorderColor="Black" BorderStyle="Inset" BorderWidth="1px" BackColor="#556b7f" ForeColor="White" Width="30px" Visible="False" TabIndex="27"></asp:TextBox><br />
                                        <hr />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top;">Время, затра- ченное на ввод в эксплуа- тацию после тести- рования (реали- зации) процесса</td>
                                </tr>
                            </table>
                        </HeaderTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Общее время от передачи на реали- зацию Логики до ввода в эксплуа- тацию">
                        <ItemTemplate>
                            <asp:Label ID="Label21" runat="server" Text='<%# Bind("daystaken") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderTemplate>
                            <table class="table1" style="width: 100%; height: 255px">
                                <tr>
                                    <td style="vertical-align: top;">
                                        <asp:TextBox ID="TextBox42" runat="server" BorderColor="Black" BorderStyle="Inset" BorderWidth="1px" BackColor="#556b7f" ForeColor="White" Width="30px" Visible="False" TabIndex="27"></asp:TextBox><br />
                                        <hr />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top;">Общее время от передачи на реали- зацию Логики до ввода в эксплуа- тацию</td>
                                </tr>
                            </table>
                        </HeaderTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Правка / Удалить" ShowHeader="False">
                        <EditItemTemplate>
                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" CommandName="Update" Text="Обновить"></asp:LinkButton>
                        </EditItemTemplate>
                        <HeaderTemplate>
                            <table class="table1" style="width: 100%; height: 255px">
                                <tr>
                                    <td style="vertical-align: top;">&nbsp;
                       Поиск:&nbsp;<asp:Button ID="Button8" runat="server" OnClick="Button8_Click" Text="🔍" BorderColor="Black" BorderStyle="Inset" BorderWidth="1px" BackColor="#556b7f" ForeColor="White" Width="24px" TabIndex="29" /><br />
                                        <hr />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="vertical-align: top;">Правка / Удалить</td>
                                </tr>
                            </table>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Edit">
                                <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/upd.png" Width="20px" />
                                Правка
                            </asp:LinkButton>
                            &nbsp;
                            <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Delete">
                                <asp:Image ID="del" runat="server" ImageUrl="~/Images/del.png" Width="20px" />
                                Удалить
                            </asp:LinkButton>
                        </ItemTemplate>
                        <ControlStyle Width="80px" />
                    </asp:TemplateField>
                </Columns>
                <EmptyDataRowStyle BorderStyle="Solid" />
                <HeaderStyle CssClass="DGIGridStyleHeader" Font-Bold="True" HorizontalAlign="Center" VerticalAlign="Middle"></HeaderStyle>
                <PagerStyle BackColor="white" ForeColor="Black" HorizontalAlign="center" Font-Size="Larger" CssClass="pagerStyle" />
                <RowStyle HorizontalAlign="Center" />
            </asp:GridView>


            <asp:Label ID="Label12" runat="server" Text="Значений не найдено. Повторите поиск" BackColor="#ffcccc"></asp:Label>
            <br />
            <br />

            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" SelectCommand="SELECT &quot;A&quot;, &quot;B&quot;, &quot;C&quot;, &quot;D&quot;, &quot;E&quot;, &quot;F&quot;, &quot;G&quot;, &quot;H&quot;, &quot;I&quot;, &quot;J&quot;, &quot;K&quot;, &quot;L&quot;, &quot;M&quot;, &quot;N&quot;, &quot;O&quot;, &quot;P&quot;, &quot;Q&quot;, &quot;R&quot;, &quot;S&quot;, &quot;T&quot; FROM &quot;LOGICS3&quot;"></asp:SqlDataSource>
        </div>
    </div>
</asp:Content>


