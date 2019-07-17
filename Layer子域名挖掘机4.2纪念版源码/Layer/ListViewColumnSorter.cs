using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Layer
{
    class ListViewColumnSorter : IComparer
    {
        private int ColumnToSort;// 指定按照哪个列排序      
        private SortOrder OrderOfSort;// 指定排序的方式               
        private CaseInsensitiveComparer ObjectCompare;// 声明CaseInsensitiveComparer类对象，
        public ListViewColumnSorter()// 构造函数
        {
            ColumnToSort = 0;// 默认按第一列排序            
            OrderOfSort = SortOrder.None;// 排序方式为不排序            
            ObjectCompare = new CaseInsensitiveComparer();// 初始化CaseInsensitiveComparer类对象
        }
        // 重写IComparer接口.        
        // <returns>比较的结果.如果相等返回0，如果x大于y返回1，如果x小于y返回-1</returns>
        public int Compare(object x, object y)
        {
            int compareResult;
            ListViewItem listviewX, listviewY;
            // 将比较对象转换为ListViewItem对象
            listviewX = (ListViewItem)x;
            listviewY = (ListViewItem)y;
            // 比较
            compareResult = ObjectCompare.Compare(listviewX.SubItems[ColumnToSort].Text, listviewY.SubItems[ColumnToSort].Text);
            // 根据上面的比较结果返回正确的比较结果
            if (OrderOfSort == SortOrder.Ascending)
            {   // 因为是正序排序，所以直接返回结果
                return compareResult;
            }
            else if (OrderOfSort == SortOrder.Descending)
            {  // 如果是反序排序，所以要取负值再返回
                return (-compareResult);
            }
            else
            {
                // 如果相等返回0
                return 0;
            }
        }
        /// 获取或设置按照哪一列排序.        
        public int SortColumn
        {
            set
            {
                ColumnToSort = value;
            }
            get
            {
                return ColumnToSort;
            }
        }
        /// 获取或设置排序方式.    
        public SortOrder Order
        {
            set
            {
                OrderOfSort = value;
            }
            get
            {
                return OrderOfSort;
            }
        }
    }
}
