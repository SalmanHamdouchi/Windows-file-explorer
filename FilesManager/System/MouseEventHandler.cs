using System.Windows.Forms;

namespace System
{
    internal class MouseEventHandler
    {
        private Action<object, MouseEventArgs> itemsListView_DoubleClick;

        public MouseEventHandler(Action<object, MouseEventArgs> itemsListView_DoubleClick)
        {
            this.itemsListView_DoubleClick = itemsListView_DoubleClick;
        }
    }
}