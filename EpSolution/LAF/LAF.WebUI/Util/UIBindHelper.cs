using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace LAF.WebUI.Util
{
    /// <summary>
    /// 通用类库-Web数据绑定         
    /// </summary>
    public class UIBindHelper
    {
        #region 把表单值绑定到业务对象
        /// <summary>
        /// 把表单中的数据绑定到业务对象的非静态公共属性上
        /// 要求表单名和对应的业务对象的属性名一致。

        /// </summary>
        /// <param name="request">HttpRequest对象的一个集合</param>
        /// <param name="model">业务对象</param>
        public static void BindModelByRequest(HttpRequest request, object model)
        {
            if (model == null) return;

            PropertyInfo[] Properties = model.GetType().GetProperties(BindingFlags.Instance |
                BindingFlags.Public);
            string FormNames = String.Join(",", request.Form.AllKeys).ToLower();

            string Value = null;
            foreach (PropertyInfo Property in Properties)
            {
                if (FormNames.IndexOf(Property.Name.ToLower()) >= 0)
                {
                    Value = request.Form[Property.Name];
                    //当输入空字符串时，除字符型以外的类型转换大多会产生异常

                    if (Value == "" && Property.PropertyType != typeof(string)) continue;
                    Property.SetValue(model, Convert.ChangeType(Value, Property.PropertyType), null);
                }
            }
        }

        public static void BindModelByRequestQueryString(HttpRequest request, object model)
        {
            if (model == null) return;

            PropertyInfo[] Properties = model.GetType().GetProperties(BindingFlags.Instance |
                BindingFlags.Public);
            string FormNames = String.Join(",", request.QueryString.AllKeys).ToLower();

            string Value = null;
            foreach (PropertyInfo Property in Properties)
            {
                if (FormNames.IndexOf(Property.Name.ToLower()) >= 0)
                {
                    Value = request.QueryString[Property.Name];
                    //当输入空字符串时，除字符型以外的类型转换大多会产生异常

                    if (Value == "" && Property.PropertyType != typeof(string)) continue;
                    Property.SetValue(model, Convert.ChangeType(Value, Property.PropertyType), null);
                }
            }
        }


        /// <summary>
        /// 把表单中的数据绑定到业务对象的非静态公共属性上
        /// 要求表单名和对应的业务对象的属性名一致。

        /// </summary>
        /// <param name="request">HttpRequest对象的一个集合</param>
        /// <param name="model">业务对象</param>
        /// <param name="isThrow">是否抛出异常</param>
        public static void BindModelByRequest(HttpRequest request, object model, bool isThrow)
        {
            if (model == null) return;

            PropertyInfo[] Properties = model.GetType().GetProperties(BindingFlags.Instance |
                BindingFlags.Public);
            string FormNames = String.Join(",", request.Form.AllKeys).ToLower();

            string Value = null;
            foreach (PropertyInfo Property in Properties)
            {
                if (FormNames.IndexOf(Property.Name.ToLower()) >= 0)
                {
                    Value = request.Form[Property.Name];
                    //当输入空字符串时，除字符型以外的类型转换大多会产生异常

                    if (Value == "" && Property.PropertyType != typeof(string)) continue;
                    try
                    {
                        Property.SetValue(model, Convert.ChangeType(Value, Property.PropertyType), null);
                    }
                    catch
                    {
                        if (isThrow)
                            throw;
                        else
                            continue;
                    }
                }
            }
        }


        /// <summary>
        ///把表单中的数据绑定到业务对象的非静态公共属性上
        /// 要求表单名和对应的业务对象的属性名一致。

        /// </summary>
        /// <param name="container">控件容器，一般是Page对象或Panel对象</param>
        /// <param name="model">业务对象</param>
        public static void BindModelByControls(Control container, object model)
        {
            if (model == null) return;

            PropertyInfo[] Properties = model.GetType().GetProperties(BindingFlags.Instance |
                BindingFlags.Public);

            object Value = null;
            foreach (PropertyInfo Property in Properties)
            {
                Control Ctrl = container.FindControl(Property.Name);
                if (Ctrl != null)
                {
                    if (Ctrl is TextBox)
                        Value = (Ctrl as TextBox).Text;
                    else if (Ctrl is ListControl) 	 //DropDownList、RadioButtonList、CheckBoxList 和 ListBox			 
                        Value = (Ctrl as ListControl).SelectedValue;
                    else if (Ctrl is CheckBox)
                        Value = (Ctrl as CheckBox).Checked;
                    else if (Ctrl is Calendar)
                        Value = (Ctrl as Calendar).SelectedDate;
                    else if (Ctrl is Literal)
                        Value = (Ctrl as Literal).Text;
                    else if (Ctrl is Label)
                        Value = (Ctrl as Label).Text;
                    else if (Ctrl is HiddenField)
                        Value = (Ctrl as HiddenField).Value;
                    else if (Ctrl is HtmlInputHidden)//隐藏域需设为“做为服务器控件运行"
                        Value = (Ctrl as HtmlInputHidden).Value;
                    else
                        continue;
                    if (Value != null)
                    {
                        if (Value.ToString() == "" && Property.PropertyType != typeof(string)) continue;
                    }


                    Property.SetValue(model, Convert.ChangeType(Value, Property.PropertyType), null);
                }

            }

        }


        /// <summary>
        ///把表单中的数据绑定到业务对象的非静态公共属性上
        /// 要求表单名和对应的业务对象的属性名一致。

        /// </summary>
        /// <param name="container">控件容器，一般是Page对象或Panel对象</param>
        /// <param name="model">业务对象</param>
        /// <param name="isThrow">是否抛出异常</param>
        public static void BindModelByControls(Control container, object model, bool isThrow)
        {
            if (model == null) return;

            PropertyInfo[] Properties = model.GetType().GetProperties(BindingFlags.Instance |
                BindingFlags.Public);

            object Value = null;
            foreach (PropertyInfo Property in Properties)
            {
                Control Ctrl = container.FindControl(Property.Name);
                if (Ctrl != null)
                {
                    if (Ctrl is TextBox)
                        Value = (Ctrl as TextBox).Text;
                    else if (Ctrl is ListControl)  //DropDownList、RadioButtonList、CheckBoxList 和 ListBox				 
                        Value = (Ctrl as ListControl).SelectedValue;
                    else if (Ctrl is CheckBox)
                        Value = (Ctrl as CheckBox).Checked;
                    else if (Ctrl is Calendar)
                        Value = (Ctrl as Calendar).SelectedDate;
                    else if (Ctrl is Literal)
                        Value = (Ctrl as Literal).Text;
                    else if (Ctrl is Label)
                        Value = (Ctrl as Label).Text;
                    else if (Ctrl is HtmlInputHidden)//隐藏域需设为“做为服务器控件运行"
                        Value = (Ctrl as HtmlInputHidden).Value;
                    else
                        continue;
                    if (Value != null)
                    {
                        if (Value.ToString() == "" && Property.PropertyType != typeof(string)) continue;
                    }

                    try
                    {
                        Property.SetValue(model, Convert.ChangeType(Value, Property.PropertyType), null);
                    }
                    catch
                    {
                        if (isThrow)
                            throw;
                        else
                            continue;
                    }
                }

            }

        }


        /// <summary>
        /// 把DataGrid中当前编辑行中的绑定列的值绑定到业务对象
        /// </summary>
        /// <param name="grid">DataGrid的实例</param>
        /// <param name="e">包含当前编辑行信息的事件参数</param>
        /// <param name="model">业务对象</param>
        public static void BindModelByDataGrid(DataGrid grid, DataGridItem item, object model)
        {
            PropertyInfo[] Properties = model.GetType().GetProperties(BindingFlags.Instance |
                BindingFlags.Public);


            for (int i = 0; i < grid.Columns.Count; i++)
            {
                if (grid.Columns[i] is BoundColumn)
                {
                    BoundColumn BC = grid.Columns[i] as BoundColumn;
                    foreach (PropertyInfo Property in Properties)
                    {
                        if (Property.Name == BC.DataField)
                            if (item.Cells[i].HasControls())
                                Property.SetValue(model,
                                    Convert.ChangeType((item.Cells[i].Controls[0] as TextBox).Text, Property.PropertyType),
                                    null);
                            else
                                Property.SetValue(model, Convert.ChangeType(item.Cells[i].Text, Property.PropertyType), null);


                    }

                }

            }

        }


        /// <summary>
        /// 把DataGrid中当前编辑行中的绑定列的值绑定到业务对象
        /// </summary>
        /// <param name="grid">DataGrid的实例</param>
        /// <param name="e">包含当前编辑行信息的事件参数</param>
        /// <param name="model">业务对象</param>
        /// <param name="isThrow">是否抛出异常</param>
        public static void BindModelByDataGrid(DataGrid grid, DataGridItem item, object model, bool isThrow)
        {
            PropertyInfo[] Properties = model.GetType().GetProperties(BindingFlags.Instance |
                BindingFlags.Public);


            for (int i = 0; i < grid.Columns.Count; i++)
            {
                if (grid.Columns[i] is BoundColumn)
                {
                    BoundColumn BC = grid.Columns[i] as BoundColumn;
                    foreach (PropertyInfo Property in Properties)
                    {
                        if (Property.Name == BC.DataField)
                            try
                            {
                                if (item.Cells[i].HasControls())
                                    Property.SetValue(model,
                                        Convert.ChangeType((item.Cells[i].Controls[0] as TextBox).Text, Property.PropertyType),
                                        null);
                                else
                                    Property.SetValue(model, Convert.ChangeType(item.Cells[i].Text, Property.PropertyType), null);
                            }
                            catch
                            {
                                if (isThrow)
                                    throw;
                                else
                                    continue;
                            }


                    }

                }

            }

        }


        /// <summary>
        /// 把DataGrid中的数据按行赋给业务对象
        /// </summary>
        /// <param name="grid">DataGrid实例</param>
        /// <param name="model">业务对象的类型</param>
        /// <returns>业务对象的集合</returns>
        public static List<object> BindModelByDataGrid(DataGrid grid, System.Type model)
        {
            int RowsCount = -1;
            List<object> list;
            Assembly Assem;
            RowsCount = grid.Items.Count;
            //动态创建业务对象集合。

            try
            {
                Assem = Assembly.Load(model.Assembly.FullName);
                list = new List<object>();
                for (int i = 0; i < RowsCount; i++)
                {
                    list.Add(Assem.CreateInstance(model.FullName, true));
                }
            }
            catch
            {
                return null;
            }
            //获取业务类公共实例属性

            PropertyInfo[] Properties = model.GetProperties(BindingFlags.Instance |
                BindingFlags.Public);

            #region 遍历每一列赋值

            for (int i = 0; i < grid.Columns.Count; i++)
            {
                if (grid.Columns[i] is BoundColumn)
                {
                    BoundColumn BC = grid.Columns[i] as BoundColumn;
                    foreach (PropertyInfo Property in Properties)
                    {
                        if (Property.Name == BC.DataField)
                            //把该列的每一行赋值给业务对象
                            for (int j = 0; j < RowsCount; j++)
                            {

                                if (grid.Items[j].Cells[i].HasControls())
                                    Property.SetValue(list[j],
                                        Convert.ChangeType((grid.Items[j].Cells[i].Controls[0] as TextBox).Text, Property.PropertyType),
                                        null);
                                else
                                    Property.SetValue(list[j], Convert.ChangeType(grid.Items[j].Cells[i].Text, Property.PropertyType), null);


                            }

                    }

                }

            }
            #endregion 遍历每一列赋值



            return list;

        }



        /// <summary>
        /// 把DataGrid中的数据按行赋给业务对象
        /// </summary>
        /// <param name="grid">DataGrid实例</param>
        /// <param name="model">业务对象的类型</param>
        /// <param name="isThrow">是否抛出异常</param>
        /// <returns>业务对象的集合</returns>
        public static List<object> BindModelByDataGrid(DataGrid grid, System.Type model, bool isThrow)
        {
            int RowsCount = -1;
            List<object> list;
            Assembly Assem;
            RowsCount = grid.Items.Count;
            //动态创建业务对象集合。

            try
            {
                Assem = Assembly.Load(model.Assembly.FullName);
                list = new List<object>();
                for (int i = 0; i < RowsCount; i++)
                {
                    list.Add(Assem.CreateInstance(model.FullName, true));
                }
            }
            catch
            {
                return null;
            }
            //获取业务类公共实例属性

            PropertyInfo[] Properties = model.GetProperties(BindingFlags.Instance |
                BindingFlags.Public);

            #region 遍历每一列赋值

            for (int i = 0; i < grid.Columns.Count; i++)
            {
                if (grid.Columns[i] is BoundColumn)
                {
                    BoundColumn BC = grid.Columns[i] as BoundColumn;
                    foreach (PropertyInfo Property in Properties)
                    {
                        if (Property.Name == BC.DataField)
                            //把该列的每一行赋值给业务对象
                            for (int j = 0; j < RowsCount; j++)
                            {
                                try
                                {
                                    if (grid.Items[j].Cells[i].HasControls())
                                        Property.SetValue(list[j],
                                            Convert.ChangeType((grid.Items[j].Cells[i].Controls[0] as TextBox).Text, Property.PropertyType),
                                            null);
                                    else
                                        Property.SetValue(list[j], Convert.ChangeType(grid.Items[j].Cells[i].Text, Property.PropertyType), null);
                                }
                                catch
                                {
                                    if (isThrow)
                                        throw;
                                    else
                                        continue;
                                }
                            }

                    }

                }

            }
            #endregion 遍历每一列赋值



            return list;

        }



        #endregion 把表单值绑定到业务对象

        #region 把业务对象的属性绑定到表单
        /// <summary>
        /// 把业务对象的公开实例属性绑定到表单域

        /// 要求表单名和对应的业务对象的属性名一致。

        /// 此外注意表单值要对应bool值时，注意首字母大写，如True,False
        /// </summary>
        /// <param name="container">控件容器，一般是Page对象或Panel对象</param>
        /// <param name="model">业务对象</param>
        public static void BindForm(Control container, object model)
        {
            if (model == null) return;

            PropertyInfo[] Properties = model.GetType().GetProperties(BindingFlags.Instance |
                BindingFlags.Public);

            object Value = null;
            foreach (PropertyInfo Property in Properties)
            {
                try
                {
                    #region 绑定
                    Control Ctrl = container.FindControl(Property.Name);
                    if (Ctrl != null)
                    {
                        //取得属性值

                        Value = Property.GetValue(model, null);

                        if (Value == null)
                            continue;

                        if (Ctrl is TextBox)
                        {
                            if (Value != null)
                            {
                                (Ctrl as TextBox).Text = Value.ToString();
                            }

                        }

                        else if (Ctrl is ListControl) //DropDownList、RadioButtonList、CheckBoxList 和 ListBox	
                        {

                            //!!!bool值的ToString方法返回的是True或False,而不是false,true,FALSE,TRUE
                            //所以相关控件的Value要注意首字母大写
                            ListItem listItem = (Ctrl as ListControl).Items.FindByValue(Convert.ToString(Value));
                            if (listItem != null)
                            {
                                listItem.Selected = true;
                            }

                        }
                        else if (Ctrl is CheckBox)
                        {
                            (Ctrl as CheckBox).Checked = (bool)Value;
                        }

                        else if (Ctrl is Calendar)
                        {
                            (Ctrl as Calendar).SelectedDate = (System.DateTime)Value;
                            (Ctrl as Calendar).VisibleDate = (System.DateTime)Value;
                        }
                        else if (Ctrl is Literal)
                        {
                            (Ctrl as Literal).Text = (string)Value;
                        }

                        else if (Ctrl is Label)
                        {
                            (Ctrl as Label).Text = Value.ToString();
                        }

                        else if (Ctrl is HiddenField)
                        {
                            (Ctrl as HiddenField).Value = Value.ToString();
                        }

                    }
                    //绑定数据
                    #endregion 绑定
                }
                catch
                {
                    continue;
                }
                //container.DataBind();
            }
        }

        #endregion 把业务对象的属性绑定到表单
    }
}
