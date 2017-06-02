using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MW.Budgeting.UI.Controls.Grids
{
    public class ComboColumn<T> : DataGridViewColumn
    {
        public ComboColumn() : base(new ComboCell<T>())
        {

        }
    }

    public class ComboCell<T> : DataGridViewTextBoxCell
    {
        public ComboCell() : base()
        {
            
        }

        public override Type EditType => typeof(ComboEditor<T>);
        public override Type ValueType => typeof(string);
        public override object DefaultNewRowValue => string.Empty;

        public override void InitializeEditingControl(int rowIndex, object initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
        {
            base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);
        }

        public bool AddValue(T item)
        {
            try
            {
                ComboEditor<T> editor = DataGridView.EditingControl as ComboEditor<T>;
                editor.AddValue(item);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool RemoveValue(T item)
        {
            try
            {
                ComboEditor<T> editor = DataGridView.EditingControl as ComboEditor<T>;
                editor.RemoveValue(item);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


    }

    public class ComboEditor<T> : ComboBox, IDataGridViewEditingControl
    {
        public ComboEditor()
        {
            ValueItems = new List<T>();
        }

        public List<T> ValueItems { get; set; }

        protected override void OnValueMemberChanged(EventArgs eventargs)
        {
            // This one is important. It tells the grid that something changed within it. This is very usual for saving things. :)
            EditingControlValueChanged = true;
            this.EditingControlDataGridView.NotifyCurrentCellDirty(true);
            base.OnValueMemberChanged(eventargs);
        }

        public bool AddValue(T item)
        {
            try
            {
                this.ValueItems.Add(item);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool RemoveValue(T item)
        {
            try
            {
                this.ValueItems.Remove(item);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        #region IDataGridViewEditingControl-Implementation

        public object EditingControlFormattedValue
        {
            get
            {
                if (ValueItems.Count == 0)
                    return string.Empty;

                return ValueItems[0].ToString();
            }
            set
            {
                // Probably not needed as you don't add stuff into combo boxes, but if we don't have it it gets angry at us :x
            }
        }

        public DataGridView EditingControlDataGridView { get; set; }
        public int EditingControlRowIndex { get; set; }
        public bool EditingControlValueChanged { get; set; }

        public Cursor EditingPanelCursor { get { return base.Cursor; } }

        public bool RepositionEditingControlOnValueChange { get { return false; } }

        public void ApplyCellStyleToEditingControl(DataGridViewCellStyle dataGridViewCellStyle)
        {
            
        }

        public bool EditingControlWantsInputKey(Keys keyData, bool dataGridViewWantsInputKey)
        {
            return true;
        }

        public object GetEditingControlFormattedValue(DataGridViewDataErrorContexts context)
        {
            return EditingControlFormattedValue;
        }

        public void PrepareEditingControlForEdit(bool selectAll)
        {
            
        }

        #endregion
    }
}
