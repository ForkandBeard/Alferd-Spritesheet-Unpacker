using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace ForkandBeard.Util.UI
{
    public class ListViewSorter : IComparer
    {
        /// <summary>
        /// Case insensitive comparer object
        /// </summary>
        private CaseInsensitiveComparer ObjectCompare;

        /// <summary>
        /// Gets or sets the number of the column to which to apply the sorting operation (Defaults to '0').
        /// </summary>
        public int SortColumn { get; set; }

        /// <summary>
        /// Gets or sets the order of sorting to apply (for example, 'Ascending' or 'Descending').
        /// </summary>
        public SortOrder Order { get; set; }

        public ListViewSorter()
        {
            // Initialize the column to '0'
            this.SortColumn = 0;
            // Initialize the sort order to 'none'
            this.Order = SortOrder.None;
            // Initialize the CaseInsensitiveComparer object
            this.ObjectCompare = new CaseInsensitiveComparer();
        }

        public static void SortListViewColumn(System.Windows.Forms.ListView listView, int columnIndex, SortOrder order)
        {
            ListViewSorter sorter;
            if (listView.ListViewItemSorter == null)
            {
                ListView.MakeListViewSortable(listView);
            }            
            sorter = (ListViewSorter)listView.ListViewItemSorter;

            sorter.Order = order;
            sorter.SortColumn = columnIndex;
            listView.Sort();
        }

        public void HandleColumnClicked(System.Windows.Forms.ListView listView, ColumnClickEventArgs e)
        {
            // Determine if clicked column is already the column that is being sorted.
            if (e.Column == this.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (this.Order == SortOrder.Ascending)
                {
                    this.Order = SortOrder.Descending;
                }
                else
                {
                    this.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                this.SortColumn = e.Column;
                this.Order = SortOrder.Ascending;
            }

            // Perform the sort with these new sort options.
            listView.Sort();            
        }

        public int Compare(object x, object y)
        {
            int compareResult;
            ListViewItem listviewX, listviewY;
            string textA;
            string textB;
            DateTime dateA;
            DateTime dateB;
            decimal numA;
            decimal numB;

            // Cast the objects to be compared to ListViewItem objects
            listviewX = (ListViewItem)x;
            listviewY = (ListViewItem)y;

            textA = listviewX.SubItems[this.SortColumn].Text;
            textB = listviewY.SubItems[this.SortColumn].Text;

            // Compare the two items
            if (
                DateTime.TryParse(textA, out dateA)
                && DateTime.TryParse(textB, out dateB)
                )
            {
                compareResult = Convert.ToInt32(dateA.Subtract(dateB).TotalSeconds);
            }
            else if (
                    Decimal.TryParse(textA, out numA)
                    && Decimal.TryParse(textB, out numB)
                    )
            {
                compareResult = Decimal.Compare(numA, numB);
            }
            else
            {
                compareResult = this.ObjectCompare.Compare(textA, textB);
            }

            // Calculate correct return value based on object comparison
            if (this.Order == SortOrder.Ascending)
            {
                // Ascending sort is selected, return normal result of compare operation
                return compareResult;
            }
            else if (this.Order == SortOrder.Descending)
            {
                // Descending sort is selected, return negative result of compare operation
                return (-compareResult);
            }
            else
            {
                // Return '0' to indicate they are equal
                return 0;
            }
        }
    }
}
