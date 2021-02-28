using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Zamagon.WPF
{
    public abstract class PropertyChangedBase : INotifyPropertyChanged
    {
        #region ProperyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged([CallerMemberName] string propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        #endregion
    }
}
