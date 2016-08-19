﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using FrameworkControls.Dialogs;

namespace RawMaterialManagement.Items_Management
{
    public partial class ManageItem : FrameworkControls.Forms.TableWindowBase
    {
        public ManageItem() : base()
        {
            InitializeComponent();

            MySqlCommand sc = new MySqlCommand("select * from raw_item_tab", con);
            
            dataAdapter.SelectCommand = sc;
            dataAdapter.Fill(dataSet);
            bindingSource.DataSource = dataSet.Tables[0];

            MySqlCommand ic = new MySqlCommand
                ("insert into raw_item_tab (name,description,stock_level,unit_of_measure,item_category,bar_code) values (@name,@description,@stock_level,@unit_of_measure,@item_category,@bar_code)", con);
            //insertCommand.Parameters.Add("@itemid", MySqlDbType.Int32, 200, null);
            ic.Parameters.Add("@name", MySqlDbType.VarChar, 2000, "name");
            ic.Parameters.Add("@description", MySqlDbType.VarChar, 2000, "description");
            ic.Parameters.Add("@stock_level", MySqlDbType.Int32, 11, "stock_level");
            ic.Parameters.Add("@unit_of_measure", MySqlDbType.VarChar, 20, "unit_of_measure");
            ic.Parameters.Add("@item_category", MySqlDbType.VarChar, 200, "item_category");
            ic.Parameters.Add("@bar_code", MySqlDbType.VarChar, 200, "bar_code");

            dataAdapter.InsertCommand = ic;

            MySqlCommand uc = new MySqlCommand
                ("update raw_item_tab set name = @itemname, description = @description, stock_level = @stock_level, unit_of_measure = @unit_of_measure, item_category = @item_category, bar_code = @bar_code where item_id = @itemid", con);
            uc.Parameters.Add("@itemname", MySqlDbType.VarChar, 200, "name");
            uc.Parameters.Add("@description", MySqlDbType.VarChar, 2000, "description");
            uc.Parameters.Add("@stock_level", MySqlDbType.Int32, 11, "stock_level");
            uc.Parameters.Add("@unit_of_measure", MySqlDbType.VarChar, 20, "unit_of_measure");
            uc.Parameters.Add("@item_category", MySqlDbType.VarChar, 200, "item_category");
            uc.Parameters.Add("@bar_code", MySqlDbType.VarChar, 200, "bar_code");
            uc.Parameters.Add("@itemid", MySqlDbType.Int32, 200, "item_id");

            dataAdapter.UpdateCommand = uc;

            MySqlCommand dc = new MySqlCommand("delete from raw_item_tab where item_id = @itemid", con);
            dc.Parameters.Add("@itemid", MySqlDbType.Int32, 200, "item_id");

            dataAdapter.DeleteCommand = dc;

        }

        protected override void Search()
        {
            SearchDialog dlg = new SearchDialog(new MySqlCommand("select * from raw_item_tab",con));
            dlg.ShowDialog();
        }
    }
}
