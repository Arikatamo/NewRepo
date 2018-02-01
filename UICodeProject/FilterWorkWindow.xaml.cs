using ExampleCodeFirst.Entities;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace UICodeProject
{
    /// <summary>
    /// Interaction logic for FilterWorkWindow.xaml
    /// </summary>
    public partial class FilterWorkWindow : MetroWindow
    {

        List<ParentNewItem> parentItems = new List<ParentNewItem>();
        public FilterWorkWindow()
        {
            InitializeComponent();

        }

        private void btnAddFilter_Click(object sender, RoutedEventArgs e)
        {
             using (EFContext context = new EFContext())
            {
                string text = txtFilterName.Text;

                var find = context.FilterName.SingleOrDefault(f => f.Name == text);
                if (find == null)
                {
                    FilterName filterName = new FilterName
                    {
                        Name = text
                    };
                    context.FilterName.Add(filterName);
                    context.SaveChanges();
                    MyTreeViewItem viewItem = new MyTreeViewItem()
                    {
                        Id = filterName.Id.ToString(),
                        Name = filterName.Name
                    };
                    TreeViewItem item = new TreeViewItem();
                    item.Header = viewItem;
                    TreeViewFilterName.Items.Add(item);
                }
                else
                {
                    MessageBox.Show($"Find {find.Id}");
                }

            }

        }

        private void btnAddValue_Click(object sender, RoutedEventArgs e)
        {
            string text = txtFilterValue.Text;

            if (TreeViewFilterName.SelectedItem is TreeViewItem && !string.IsNullOrEmpty(text))
            {
                TreeViewItem newChild = new TreeViewItem()
                {
                    Header = text
                };
                TreeViewItem it = TreeViewFilterName.SelectedItem as TreeViewItem;
                ItemsControl parent = GetSelectedTreeViewItemParent(it);
                MyTreeViewItem MyHeader = new MyTreeViewItem();
                if (parent is TreeViewItem)
                {
                    TreeViewItem par = parent as TreeViewItem;
                    MyHeader = par.Header as MyTreeViewItem;
                    par.Items.Add(newChild);
                }
                else
                {
                    it.Items.Add(newChild);
                    MyHeader = it.Header as MyTreeViewItem;
                }



                using (EFContext context = new EFContext())
                {
                    FilterValue filtervalue = new FilterValue()
                    {
                        Name = newChild.Header.ToString()
                    };
                    context.FilterValue.Add(filtervalue);
                    context.SaveChanges();
                    FilterNameGroups group = new FilterNameGroups()
                    {
                        FilterNameId = int.Parse(MyHeader.Id.ToString()),
                        FilterValueId = filtervalue.Id
                    };
                    context.FilterNameGroups.Add(group);
                    context.SaveChanges();
                }
                RefreshTreeView();
            }
        }
        public ItemsControl GetSelectedTreeViewItemParent(TreeViewItem item)
        {
            DependencyObject parent = VisualTreeHelper.GetParent(item);
            while (!(parent is TreeViewItem || parent is TreeView))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }

            return parent as ItemsControl;
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            using (EFContext context = new EFContext())
            {
                foreach (var item in parentItems)
                {
                    if (item.children.Count > 0)
                    {
                        using (TransactionScope trans = new TransactionScope())
                        {
                            FilterName filterName = new FilterName
                            {
                                Name = item.Name
                            };
                            context.FilterName.Add(filterName);
                            context.SaveChanges();
                            foreach (var children in item.children)
                            {
                                FilterValue filtervalue = new FilterValue()
                                {
                                    Name = children
                                };
                                context.FilterValue.Add(filtervalue);
                                context.SaveChanges();
                                FilterNameGroups group = new FilterNameGroups()
                                {
                                    FilterNameId = filterName.Id,
                                    FilterValueId = filtervalue.Id
                                };
                                context.FilterNameGroups.Add(group);
                                context.SaveChanges();
                            }
                            trans.Complete();
                            RefreshTreeView();
                        }
                    }
                }
            }
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshTreeView();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var item = TreeViewFilterName.SelectedItem as TreeViewItem;
            using (EFContext context = new EFContext())
            {
                if (item != null)
                {
                    ItemsControl parent = GetSelectedTreeViewItemParent(item as TreeViewItem);
                    if (parent != null)
                    {
                        MyTreeViewItem myItem = item.Header as MyTreeViewItem;
                        MyTreeViewItem parentItem = (parent as TreeViewItem).Header as MyTreeViewItem;
                        var deletevalue = context.FilterValue.SingleOrDefault(x => x.Id.ToString() == myItem.Id);
                        var delgroup = context.FilterNameGroups.SingleOrDefault
                            (
                            x => x.FilterNameId.ToString() == parentItem.Id
                            && x.FilterValueId == deletevalue.Id
                            );

                        if (deletevalue != null)
                        {
                            context.FilterNameGroups.Remove(delgroup);
                            context.FilterValue.Remove(deletevalue);
                            context.SaveChanges();
                            RefreshTreeView();
                        }


                    }
                }



            }
        }

        private void RefreshTreeView()
        {
            TreeViewFilterName.Items.Clear();
            TreeViewFilterName.DisplayMemberPath = "Name";
            TreeViewFilterName.SelectedValuePath = "Id";

            using (EFContext context = new EFContext())
            {
                var query = (from f in context.vFilterNameGroup
                             select new
                             {
                                 FNameId = f.FilterNameId,
                                 FName = f.FilterName,
                                 FvalueId = f.FilterValueId,
                                 Fvalue = f.FilterValue
                             }).ToList();
                var group = (from f in query
                             group f by new
                             {
                                 Id = f.FNameId,
                                 Name = f.FName
                             } into g
                             orderby g.Key.Name
                             select g).ToList();

                foreach (var filterName in group)
                {
                    var Fname = new MyTreeViewItem
                    {
                        Id = filterName.Key.Id.ToString(),
                        Name = filterName.Key.Name
                    };
                    TreeViewItem parent = new TreeViewItem();
                    parent.Header = Fname;/* = filterName.Key.Name;*/
                    TreeViewFilterName.Items.Add(parent);
                    // item.DisplayMemberPath = "name";

                    var fValues = from v in filterName
                                  group v by new MyTreeViewItem
                                  {
                                      Id = v.FvalueId.ToString(),
                                      Name = v.Fvalue
                                  };
                    foreach (var filterValue in fValues)
                    {
                        if (string.IsNullOrEmpty(filterValue.Key.Name))
                            continue;

                        var FVAL = new MyTreeViewItem
                        {
                            Id = filterValue.Key.Id,
                            Name = filterValue.Key.Name
                        };
                        TreeViewItem child = new TreeViewItem() { Header = FVAL };
                        parent.Items.Add(child);
                    }
                }
            }
        }
    }
}