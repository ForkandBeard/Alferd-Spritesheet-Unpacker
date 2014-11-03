using System;
using System.Collections.Generic;
using System.Text;

namespace ForkandBeard.Util.UI
{
    public class ListView
    {
        public static void DecheckAllItems(System.Windows.Forms.ListView listView)
        {
            foreach (System.Windows.Forms.ListViewItem item in listView.Items)
            {
                item.Checked = false;
            }
        }

        public static void DeselectAllItems(System.Windows.Forms.ListView listView)
        {
            foreach (System.Windows.Forms.ListViewItem item in listView.Items)
            {
                item.Selected = false;
            }
        }

        public static void AutoSizeColumns(System.Windows.Forms.ListView listView)
        {
            int width;
            foreach (System.Windows.Forms.ColumnHeader header in listView.Columns)
            {
                header.Width = -1; // Max content width.
                width = header.Width;
                header.Width = -2; // ColumnHeader width.
                header.Width = Math.Max(header.Width, width);
            }
        }

        public static void MakeListViewSortable(System.Windows.Forms.ListView listView)
        {
            listView.ListViewItemSorter = new ListViewSorter();
            listView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Clickable;
            listView.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(taskListView_ColumnClick);            
        }

        private static void taskListView_ColumnClick(object sender, System.Windows.Forms.ColumnClickEventArgs e)
        {
            System.Windows.Forms.ListView listView;
            ListViewSorter sorter;
            listView = (System.Windows.Forms.ListView)sender;
            sorter = (ListViewSorter)listView.ListViewItemSorter;

            sorter.HandleColumnClicked(listView, e);
        }
    }
}
