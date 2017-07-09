using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GS_DataGridCalendarCell
{
    internal class GS_CalendarEditingControl : DateTimePicker, IDataGridViewEditingControl
    {
        private DataGridView dataGridView;
        private int rowIndex;
        private bool valueChanged;

        public GS_CalendarEditingControl()
        {
            this.Format = DateTimePickerFormat.Short;

        }
        public DataGridView EditingControlDataGridView
        {
            get
            {
                return dataGridView;
            }
            set
            {
                dataGridView = value;
            }
        }

        public object EditingControlFormattedValue
        {
            get
            {
                return this.Value.ToShortDateString();
                
            }

            set
            {
                if (value is String)
                {
                    try
                    {

                        this.Value = DateTime.Parse((String)value);
                    }
                    catch
                    {
                        this.Value = DateTime.Now;
                    }
                }
            }
        }

        public int EditingControlRowIndex
        {
            get
            {
                return rowIndex;
            }
            set
            {
                rowIndex = value;
            }
        }

        public bool EditingControlValueChanged
        {
            get
            {
                return valueChanged;
            }
            set
            {
                valueChanged = value;
            }
        }

        public Cursor EditingPanelCursor
        {
            get
            {
                return base.Cursor;
            }
        }

        public bool RepositionEditingControlOnValueChange
        {
            get
            {
                return false;
            }
        }

        public void ApplyCellStyleToEditingControl(DataGridViewCellStyle dataGridViewCellStyle)
        {
            this.Font = dataGridViewCellStyle.Font;
            this.CalendarForeColor = dataGridViewCellStyle.ForeColor;
            this.CalendarMonthBackground = dataGridViewCellStyle.BackColor;
        }

        public bool EditingControlWantsInputKey(Keys keyData, bool dataGridViewWantsInputKey)
        {
            switch (keyData & Keys.KeyCode)
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

        public void PrepareEditingControlForEdit(bool selectAll)
        {

        }

        protected override void OnValueChanged(EventArgs eventargs)
        {
            // Notify the DataGridView that the contents of the cell
            // have changed.
            valueChanged = true;
            this.EditingControlDataGridView.NotifyCurrentCellDirty(true);
            base.OnValueChanged(eventargs);
        }
    }
    public class CalendarColumn : DataGridViewColumn
    {

        public CalendarColumn():base(new CalendarCell())
        {

        }
        public CalendarColumn(Action<object, EventArgs> customvaluechanged) : base(new CalendarCell(customvaluechanged))
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
                if (value != null &&
                 !value.GetType().IsAssignableFrom(typeof(CalendarCell)))
                {
                    throw new InvalidCastException("Must be a CalendarCell");
                }
                base.CellTemplate = value;
            }
        }


    }

    internal class CalendarCell : DataGridViewTextBoxCell
    {

        

        private int myVar;

        public int MyProperty
        {
            get { return myVar; }
            set { myVar = value; }
        }


        public CalendarCell():base()
        {

        }

        public CalendarCell(Action<object,EventArgs> _customvaluechanged) : base()
        {
            this.Style.Format = "d";
            CustomEvents.customvaluechanged = _customvaluechanged;
            MyProperty = 5;
        }

       

        public override void InitializeEditingControl(int rowIndex, object initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
        {
            base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);


            GS_CalendarEditingControl ctl =
            DataGridView.EditingControl as GS_CalendarEditingControl;


            ctl.ValueChanged += new EventHandler(CustomEvents.customvaluechanged);

            if (this.Value == null)
            {
                ctl.Value = (DateTime)this.DefaultNewRowValue;
                
                
                
            }
            else
            {
                if(this.Value is DateTime)
                {
                    ctl.Value = (DateTime)this.Value;
                }
                else
                {
                    ctl.Value = DateTime.Now;
                }

               
                
            }

        }

       

        public override Type EditType
        {
            get
            {
                return typeof(GS_CalendarEditingControl);
            }
        }

        public override object DefaultNewRowValue
        {
            get
            {
                //return DateTime.Now;
                return null;
            }
        }

        public override Type ValueType
        {
            get
            {
                // Return the type of the value that CalendarCell contains.

                return typeof(DateTime);
            }
        }
    }

    public class CustomEvents
    {
        private static Action<object, EventArgs> _customvaluechanged;

        public static Action<object, EventArgs> customvaluechanged
        {
            get { return _customvaluechanged; }
            set { _customvaluechanged = value; }
        }
    }
}
