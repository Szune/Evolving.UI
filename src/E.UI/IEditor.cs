using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E.UI
{
    public interface IEditor<out TElement>
    {
        TElement GetControls(object objectToEdit);
        void SaveValues(object objectToSaveTo);
    }
}
