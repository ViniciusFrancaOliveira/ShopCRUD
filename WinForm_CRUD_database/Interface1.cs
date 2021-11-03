using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListViews
{
    interface IListView
    {
        void Obtain();
        void UpdateList();
        void Clean();
    }
}
