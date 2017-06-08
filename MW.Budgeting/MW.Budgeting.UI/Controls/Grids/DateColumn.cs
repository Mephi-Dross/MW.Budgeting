using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MW.Budgeting.UI.Controls.Grids
{
    public class DateColumn : DataGridViewColumn
    {
        public DateColumn() : base(new DateCell())
        {

        }

        public override DataGridViewCell CellTemplate
        {
            get
            {
                return base.CellTemplate;
            }
            set
            {
                // Ensure that the cell used for the template is a CalendarCell.
                if (value != null && !value.GetType().IsAssignableFrom(typeof(DateCell)))
                {
                    throw new InvalidCastException("Must be a DateCell");
                }

                base.CellTemplate = value;
            }
        }
    }

    public class DateCell : DataGridViewTextBoxCell
    {
        public DateCell() : base()
        {
            // Format the datestring as a short date
            this.Style.Format = "d";
            this.Value = DateTime.Now;
        }

        public override void InitializeEditingControl(int rowIndex, object initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
        {
            base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);
            DateEditor editor = DataGridView.EditingControl as DateEditor;

            // Set the value of the editor
            if (this.Value == null || (DateTime)this.Value == DateTime.MinValue)
                editor.Value = (DateTime)this.DefaultNewRowValue;
            else
                editor.Value = (DateTime)this.Value;
        }


        // This one is interesting. These are overrides on functions that only have a getter. Apparently C# allows you to write it like this. Neat!
        public override Type EditType => typeof(DateEditor);
        public override Type ValueType => typeof(DateTime);
        public override object DefaultNewRowValue => DateTime.Now;
    }

    public class DateEditor : DateTimePicker, IDataGridViewEditingControl
    {
        public DateEditor()
        {
            // Again, we want the date to be displayed in the short date format.
            this.Format = DateTimePickerFormat.Short;
        }

        public object EditingControlFormattedValue
        {
            get
            {
                // Since we can't use a DateTime value in the list, we instead return it as string (again, short date format)
                if (this.Value == null)
                    return DateTime.Now.ToShortDateString();

                return this.Value.ToShortDateString();
            }
            set
            {
                // So obviously we need to accept the user input and try to cast it back into a date time.
                // If the user enters a wrong value, we take the current date/time instead. 
                // Ideally, we want to add something to prevent the user from entering wrong values to begin with. I've got some ideas, but need to read up on stuff first.
                if (value is String)
                {
                    try
                    {
                        if (value == null)
                            this.Value = DateTime.Now;
                        else
                            this.Value = DateTime.Parse((String)value);
                    }
                    catch
                    {
                        this.Value = DateTime.Now;
                    }
                }
            }
        }

        // Bunch of properties required for the interface, but nothing we really need to care about at this point.

        public int EditingControlRowIndex { get; set; }
        public bool RepositionEditingControlOnValueChange { get { return false; } }
        public DataGridView EditingControlDataGridView { get; set; }
        public bool EditingControlValueChanged { get; set; }
        public Cursor EditingPanelCursor { get { return base.Cursor; } }


        public void PrepareEditingControlForEdit(bool selectAll)
        {

        }

        public bool EditingControlWantsInputKey(Keys key, bool dataGridViewWantsInputKey)
        {
            // This function is responsible for catching the input and allows us to do something with it.
            // As far as I understand we want to leave these keys alone, because they get used by the DateTimePicker.
            // As for the rest, return the opposite of the input.
            // Again, I fear I don't quite understand the use of this function just yet, I simply copy pasted it from the tutorial.
            
            switch (key & Keys.KeyCode)
            {
                case Keys.Left:
                case Keys.Up:
                case Keys.Down:
                case Keys.Right:
                case Keys.Home:
                case Keys.End:
                case Keys.PageDown:
                case Keys.PageUp:
                    return true;
                default:
                    return !dataGridViewWantsInputKey;
            }
        }

        public object GetEditingControlFormattedValue(DataGridViewDataErrorContexts context)
        {
            return EditingControlFormattedValue;
        }

        public void ApplyCellStyleToEditingControl(DataGridViewCellStyle dataGridViewCellStyle)
        {
            this.Font = dataGridViewCellStyle.Font;
            this.CalendarForeColor = dataGridViewCellStyle.ForeColor;
            this.CalendarMonthBackground = dataGridViewCellStyle.BackColor;
        }

        protected override void OnValueChanged(EventArgs eventargs)
        {
            // This one is important. It tells the grid that something changed within it. This is very usual for saving things. :)
            EditingControlValueChanged = true;
            this.EditingControlDataGridView.NotifyCurrentCellDirty(true);
            base.OnValueChanged(eventargs);
        }
    }
}
