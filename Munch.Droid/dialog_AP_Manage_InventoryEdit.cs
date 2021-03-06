using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Net;

namespace Munch
{
    public class OnSignEventArgs_InventoryManagementEdit : EventArgs
    {
        private string mIngredients;
        private string mQuantity;
        private string mThreshold;
        private string mMeasureUnit;

        public string Ingredients
        {
            get { return mIngredients; }
            set { mIngredients = value; }
        }

        public string Quantity
        {
            get { return mQuantity; }
            set { mQuantity = value; }
        }

        public string Threshold
        {
            get { return mThreshold; }
            set { mThreshold = value; }
        }

        public string MeasureUnit
        {
            get { return mMeasureUnit; }
            set { mMeasureUnit = value; }
        }

        public OnSignEventArgs_InventoryManagementEdit(string ingredients, string quantity, string threshold, string measureUnit) : base()
        {
            Ingredients = ingredients;
            Quantity = quantity;
            Threshold = threshold;
            MeasureUnit = measureUnit;
        }

    }

    class dialog_AP_Manage_InventoryEdit : DialogFragment
    {
        private EditText ingredients;
        private EditText quantity;
        private EditText threshold;
        private EditText measureUnit;
        
        private Button dEditInventory;
        private Button dDeleteInventory;

        public event EventHandler<OnSignEventArgs_InventoryManagement> editItemComplete;
        public event EventHandler<OnSignEventArgs_InventoryManagement> deleteItemComplete;
        string select = (AP_MI_Activity.xcc);
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            //Load Up values from Dialog Box
            var view = inflater.Inflate(Resource.Layout.dialog_APMIEdit_, container, false);
            ingredients = view.FindViewById<EditText>(Resource.Id.txt_Edit_Ingredient);
            quantity = view.FindViewById<EditText>(Resource.Id.txt_Edit_Quantity1);
            threshold = view.FindViewById<EditText>(Resource.Id.txt_Edit_MinThreshold);
            measureUnit = view.FindViewById<EditText>(Resource.Id.txt_Edit_Unit1);
            dEditInventory = view.FindViewById<Button>(Resource.Id.btn_Edit_Inventory);
            dDeleteInventory = view.FindViewById<Button>(Resource.Id.btn_Delete_Inventory);
            ingredients.Text = select;
            //Click Event for Edit Account
            dEditInventory.Click += dEditInventory_Click;
            //Click Event for Delete Account
            dDeleteInventory.Click += dDeleteInventory_Click;
            return view;
        }
        //Edit Inventory Action
        private void dEditInventory_Click(object sender, EventArgs e)
        {
            editItemComplete.Invoke(this, new OnSignEventArgs_InventoryManagement(ingredients.Text, quantity.Text, measureUnit.Text, threshold.Text));
            var webClient = new WebClient();
            webClient.DownloadString("http://54.191.98.63/manageinventory.php?name=" + select + "&&unit=" + measureUnit.Text + "&&quantity=" + quantity.Text + "&&threshold=" + threshold.Text);
            this.Dismiss();
        }
        //Delete Inventory Action
        private void dDeleteInventory_Click(object sender, EventArgs e)
        {
            deleteItemComplete.Invoke(this, new OnSignEventArgs_InventoryManagement(ingredients.Text, quantity.Text, measureUnit.Text, threshold.Text));
            var webClient = new WebClient();
            webClient.DownloadString("http://54.191.98.63/manageinventory.php?name=" + select + "&&delete=1");
            this.Dismiss();
        }

    }
}