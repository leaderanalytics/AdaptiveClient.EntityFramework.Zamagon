using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace Zamagon.WPF
{
    public class DragAndDropListBox : ListBox
    {
        public Style DragOverStyle { get; set; }
        private Point _dragStartPoint;
        private Brush defaultBackground;

        public DragAndDropListBox()
        {
            this.PreviewMouseMove += ListBox_PreviewMouseMove;
            PreviewMouseLeftButtonDown += ListBoxItem_PreviewMouseLeftButtonDown;
            AllowDrop = true;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            Style itemContainerStyle = new Style(typeof(ListBoxItem), ItemContainerStyle);
            itemContainerStyle.Setters.Add(new Setter(ListBoxItem.AllowDropProperty, true));
            itemContainerStyle.Setters.Add(new EventSetter(ListBoxItem.DropEvent, new DragEventHandler(ListBoxItem_Drop)));
            itemContainerStyle.Setters.Add(new EventSetter(ListBoxItem.DragEnterEvent, new DragEventHandler(ListBoxItem_DragEnter)));
            itemContainerStyle.Setters.Add(new EventSetter(ListBoxItem.DragLeaveEvent, new DragEventHandler(ListBoxItem_DragLeave)));
            ItemContainerStyle = itemContainerStyle;
        }

        private P FindVisualParent<P>(DependencyObject child)  where P : DependencyObject
        {
            var parentObject = VisualTreeHelper.GetParent(child);

            if (parentObject == null)
                return null;

            P parent = parentObject as P;

            if (parent != null)
                return parent;

            return FindVisualParent<P>(parentObject);
        }

        private void ListBoxItem_DragEnter(object sender, DragEventArgs e)
        {
            ListBoxItem item = sender as ListBoxItem;
            defaultBackground = item.Background;
            item.Background = new SolidColorBrush(Colors.Orange);
        }

        private void ListBoxItem_DragLeave(object sender, DragEventArgs e)
        {
            ListBoxItem item = sender as ListBoxItem;
            item.Background = defaultBackground;
        }

        private void ListBoxItem_Drop(object sender, DragEventArgs e)
        {
            var source = e.Data.GetData(typeof(ListBoxItem)) as ListBoxItem;
            var target = ((ListBoxItem)(sender));

            int sourceIndex = this.ItemContainerGenerator.IndexFromContainer(source);
            int targetIndex = this.ItemContainerGenerator.IndexFromContainer(target);

            Move(sourceIndex, targetIndex);
            target.Background = defaultBackground;
        }

        private void Move(int sourceIndex, int targetIndex)
        {
            var items = this.ItemsSource as IList;
            object tmp = items[sourceIndex];
            items.RemoveAt(sourceIndex);
            items.Insert(targetIndex, tmp);
        }

        private void ListBox_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            Point point = e.GetPosition(null);
            Vector diff = _dragStartPoint - point;
            if (e.LeftButton == MouseButtonState.Pressed &&
                (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
            {
                var lb = sender as ListBox;
                var lbi = FindVisualParent<ListBoxItem>(((DependencyObject)e.OriginalSource));

                if (lbi != null)
                {
                    DragDrop.DoDragDrop(lbi, lbi, DragDropEffects.Move);
                }
            }
        }

        private void ListBoxItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _dragStartPoint = e.GetPosition(null);
        }
    }
}
