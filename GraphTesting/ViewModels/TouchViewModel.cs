using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Caliburn.Micro;

namespace GraphTesting.ViewModels
{
    [Export(typeof(TouchViewModel))]
    class TouchViewModel : PropertyChangedBase
    {
        [ImportingConstructor]
        public TouchViewModel(IEventAggregator ev)
        {
        }
    }
}
