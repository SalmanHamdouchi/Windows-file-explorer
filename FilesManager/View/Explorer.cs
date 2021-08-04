using FilesManager.Controller;
using FilesManager.Model;
using FilesManager.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace FilesManager.View
{
    public partial class Explorer : Form
    {
        private String folderToBeChanged;
        private Folder toBeCut;
        private ListViewItem oldItem;
        private TreeNode previousSelectedNode;
        private Folder toBeCopied;
        private Files copiedFile;
        private Files cutFile;
        private Files fileToBeChanged;

        public Explorer()
        {
            InitializeComponent();
        }
        
        private void Explorer_Load(object sender, EventArgs e)
        {
            ToolStripItem t = folderContextMenu.Items[1];
            t.Enabled = false;
            setToolStrips();
            setlistViewImgList();
            insertTreeViewImg();
            printListViewItems();
            setPath();
            SetTreeViewTheme(foldersTree.Handle);

        }

        private void setPath()
        {
            GUI.setPath(path);
        }

        private void backArrow_Click(object sender, EventArgs e)
        {
            GUI.cd("..");
            deletelistViewItems();
            printListViewItems();
        }

        private void searchTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (searchTextBox.Text != "")
            {
                deletelistViewItems();
                List<Folder> subFolders = GUI.currentFolder.searchSubFolders(GUI.currentFolder, searchTextBox.Text.ToLower());
                foreach (Folder subFolder in subFolders)
                {
                    ListViewItem item = new ListViewItem(subFolder.getFolderName());
                    if (subFolder.getId() == 226)
                    {
                        item.ImageIndex = 0;
                    }
                    else if (subFolder.getId() == 228)
                    {
                        item.ImageIndex = 1;
                    }
                    else if (subFolder.getId() == 227)
                    {
                        item.ImageIndex = 3;
                    }
                    else if (subFolder.getId() == 229)
                    {
                        item.ImageIndex = 2;
                    }
                    else if (subFolder.getId() == 230)
                    {
                        item.ImageIndex = 4;
                    }
                    else
                    {
                        item.ImageIndex = 5;
                    }

                    ItemsListView.Items.Add(item);
                }
            }
            else
            {
                deletelistViewItems();
                printListViewItems();
            }
        }
        /*---------------------- Top Panel ----------------*/
        private void homeBtnPanel_Click(object sender, EventArgs e)
        {
            homeBtnPanel.BackColor = Color.FromArgb(32, 32, 32);

            shareBtnTab.BackColor = Color.Black;
            shareBtnTab.Enabled = true;
            viewBtnTab.BackColor = Color.Black;
            viewBtnTab.Enabled = true;

            shareTopBar.Visible = false;
            homeTopBar.Visible = true;
            viewToolBar.Visible = false;

        }

        private void shareBtnTab_Click(object sender, EventArgs e)
        {
            shareBtnTab.BackColor = Color.FromArgb(32, 32, 32);

            homeBtnPanel.BackColor = Color.Black;
            homeBtnPanel.Enabled = true;
            viewBtnTab.BackColor = Color.Black;
            viewBtnTab.Enabled = true;

            homeTopBar.Visible = false;
            shareTopBar.Visible = true;

            viewToolBar.Visible = false;
            if (ItemsListView.SelectedItems.Count > 0)
                shareTopBarPic.Image = Image.FromFile((@"..\..\..\resources\shareTopBarAct.jpg"));
            else
                shareTopBarPic.Image = Image.FromFile((@"..\..\..\resources\shareTopBarDeAct.jpg"));
        }

        private void viewBtnTab_Click(object sender, EventArgs e)
        {
            viewBtnTab.BackColor = Color.FromArgb(32, 32, 32);

            homeBtnPanel.BackColor = Color.Black;
            homeBtnPanel.Enabled = true;
            shareBtnTab.BackColor = Color.Black;
            shareBtnTab.Enabled = true;

            shareTopBar.Visible = false;
            homeTopBar.Visible = false;
            viewToolBar.Visible = true;
        }
        /*---------------------- Tool Strips ----------------*/
        private void setToolStrips()
        {
            HomeToolStrip.Renderer = new MySR();
            toolStrip1.Renderer = new MySR();
            toolStrip2.Renderer = new MySR();
            toolStrip3.Renderer = new MySR();
            toolStrip4.Renderer = new MySR();
            ViewToolStrip.Renderer = new MySR();
            panesTS.Renderer = new MySR();
            iconsTS1.Renderer = new MySR();
            iconsTS2.Renderer = new MySR();
            colsTS.Renderer = new MySR();

            deactivateIcons();
            deactivatePasteIcons();

        }

        public void activateIcons()
        {
            CopyToolStrip.Enabled = true;
            deleteToolStrip.Enabled = true;

            copyToST.Enabled = true;
            cutToolStrip.Enabled = true;
            copyPathTS.Enabled = true;

            RenameToolStrip.Enabled = true;
            moveToTS.Enabled = true;
            openTS.Enabled = true;
            editTS.Enabled = true;

            CopyToolStrip.Image = Image.FromFile(@"..\..\..\resources\bv7d8-e3p9t.png");
            deleteToolStrip.Image = Image.FromFile(@"..\..\..\resources\brh0e-sqsso.png");
            copyToST.Image = Image.FromFile(@"..\..\..\resources\1935.ico");
            cutToolStrip.Image = Image.FromFile(@"..\..\..\resources\bc1sj-s9jlv.png");
            copyPathTS.Image = Image.FromFile(@"..\..\..\resources\smallCopyPath.png");
            RenameToolStrip.Image = Image.FromFile(@"..\..\..\resources\bi8ce-ilz3e.png");
            moveToTS.Image = Image.FromFile(@"..\..\..\resources\1927.ico");
            openTS.Image = Image.FromFile(@"..\..\..\resources\2215.ico");
            editTS.Image = Image.FromFile(@"..\..\..\resources\1951 - Copy.ico");

        }

        public void ativatePasteIcons()
        {
            pasteToolStrip.Enabled = true;
            pasteShortcutTS.Enabled = true;
            pasteToolStrip.Image = Image.FromFile(@"..\..\..\resources\bimra-zke5k.png");
            pasteShortcutTS.Image = Image.FromFile(@"..\..\..\resources\newPasteshrtct.png");
            folderContextMenu.Items[1].Enabled = true;
        }

        public void deactivatePasteIcons()
        {

            pasteShortcutTS.Enabled = false;
            pasteToolStrip.Enabled = false;
            folderContextMenu.Items[1].Enabled = false;
        }

        public void deactivateIcons()
        {
            cutToolStrip.Enabled = false;
            CopyToolStrip.Enabled = false;
            deleteToolStrip.Enabled = false;
            copyToST.Enabled = false;
            cutToolStrip.Enabled = false;
            copyPathTS.Enabled = false;
            RenameToolStrip.Enabled = false;
            moveToTS.Enabled = false;
            openTS.Enabled = false;
            editTS.Enabled = false;
            
        }

        private void RenameToolStrip_MouseEnter(object sender, EventArgs e)
        {
            ToolStripItem tsi = (ToolStripItem)sender;

            Bitmap bm = new Bitmap(tsi.Width, tsi.Height);
            for (int y = 0; y < tsi.Height; y++)
            {
                for (int x = 0; x < tsi.Width; x++)
                    bm.SetPixel(x, y, Color.FromArgb(80, Color.White));
            }

            tsi.BackgroundImage = bm;
        }

        private void newFolderToolStrip_MouseEnter(object sender, EventArgs e)
        {
            ToolStripItem tsi = (ToolStripItem)sender;

            Bitmap bm = new Bitmap(tsi.Width, tsi.Height);
            for (int y = 0; y < tsi.Height; y++)
            {
                for (int x = 0; x < tsi.Width; x++)
                    bm.SetPixel(x, y, Color.FromArgb(80, Color.White));
            }

            tsi.BackgroundImage = bm;
        }

        private void newItemTL_MouseEnter(object sender, EventArgs e)
        {
            ToolStripItem tsi = (ToolStripItem)sender;

            Bitmap bm = new Bitmap(tsi.Width, tsi.Height);
            for (int y = 0; y < tsi.Height; y++)
            {
                for (int x = 0; x < tsi.Width; x++)
                    bm.SetPixel(x, y, Color.FromArgb(80, Color.White));
            }

            tsi.BackgroundImage = bm;
        }

        private void EasyaccessTS_MouseEnter(object sender, EventArgs e)
        {
            ToolStripItem tsi = (ToolStripItem)sender;

            Bitmap bm = new Bitmap(tsi.Width, tsi.Height);
            for (int y = 0; y < tsi.Height; y++)
            {
                for (int x = 0; x < tsi.Width; x++)
                    bm.SetPixel(x, y, Color.FromArgb(80, Color.White));
            }

            tsi.BackgroundImage = bm;
        }

        private void openTS_MouseEnter(object sender, EventArgs e)
        {
            ToolStripItem tsi = (ToolStripItem)sender;

            Bitmap bm = new Bitmap(tsi.Width, tsi.Height);
            for (int y = 0; y < tsi.Height; y++)
            {
                for (int x = 0; x < tsi.Width; x++)
                    bm.SetPixel(x, y, Color.FromArgb(80, Color.White));
            }

            tsi.BackgroundImage = bm;
        }

        private void editTS_MouseEnter(object sender, EventArgs e)
        {
            ToolStripItem tsi = (ToolStripItem)sender;

            Bitmap bm = new Bitmap(tsi.Width, tsi.Height);
            for (int y = 0; y < tsi.Height; y++)
            {
                for (int x = 0; x < tsi.Width; x++)
                    bm.SetPixel(x, y, Color.FromArgb(80, Color.White));
            }

            tsi.BackgroundImage = bm;
        }

        private void hostoryST_MouseEnter(object sender, EventArgs e)
        {
            ToolStripItem tsi = (ToolStripItem)sender;

            Bitmap bm = new Bitmap(tsi.Width, tsi.Height);
            for (int y = 0; y < tsi.Height; y++)
            {
                for (int x = 0; x < tsi.Width; x++)
                    bm.SetPixel(x, y, Color.FromArgb(80, Color.White));
            }

            tsi.BackgroundImage = bm;
        }

        private void selectAllTS_MouseEnter(object sender, EventArgs e)
        {
            ToolStripItem tsi = (ToolStripItem)sender;

            Bitmap bm = new Bitmap(tsi.Width, tsi.Height);
            for (int y = 0; y < tsi.Height; y++)
            {
                for (int x = 0; x < tsi.Width; x++)
                    bm.SetPixel(x, y, Color.FromArgb(80, Color.White));
            }

            tsi.BackgroundImage = bm;
        }

        private void selectInvTS_MouseEnter(object sender, EventArgs e)
        {
            ToolStripItem tsi = (ToolStripItem)sender;

            Bitmap bm = new Bitmap(tsi.Width, tsi.Height);
            for (int y = 0; y < tsi.Height; y++)
            {
                for (int x = 0; x < tsi.Width; x++)
                    bm.SetPixel(x, y, Color.FromArgb(80, Color.White));
            }

            tsi.BackgroundImage = bm;
        }

        private void selectNoneTS_MouseEnter(object sender, EventArgs e)
        {
            ToolStripItem tsi = (ToolStripItem)sender;

            Bitmap bm = new Bitmap(tsi.Width, tsi.Height);
            for (int y = 0; y < tsi.Height; y++)
            {
                for (int x = 0; x < tsi.Width; x++)
                    bm.SetPixel(x, y, Color.FromArgb(80, Color.White));
            }

            tsi.BackgroundImage = bm;
        }

        private void propertiesTS_MouseEnter(object sender, EventArgs e)
        {
            ToolStripItem tsi = (ToolStripItem)sender;

            Bitmap bm = new Bitmap(tsi.Width, tsi.Height);
            for (int y = 0; y < tsi.Height; y++)
            {
                for (int x = 0; x < tsi.Width; x++)
                    bm.SetPixel(x, y, Color.FromArgb(80, Color.White));
            }

            tsi.BackgroundImage = bm;
        }

        private void copyToST_MouseEnter(object sender, EventArgs e)
        {
            ToolStripItem tsi = (ToolStripItem)sender;

            Bitmap bm = new Bitmap(tsi.Width, tsi.Height);
            for (int y = 0; y < tsi.Height; y++)
            {
                for (int x = 0; x < tsi.Width; x++)
                    bm.SetPixel(x, y, Color.FromArgb(80, Color.White));
            }

            tsi.BackgroundImage = bm;
        }

        private void moveToTS_MouseEnter(object sender, EventArgs e)
        {
            ToolStripItem tsi = (ToolStripItem)sender;

            Bitmap bm = new Bitmap(tsi.Width, tsi.Height);
            for (int y = 0; y < tsi.Height; y++)
            {
                for (int x = 0; x < tsi.Width; x++)
                    bm.SetPixel(x, y, Color.FromArgb(80, Color.White));
            }

            tsi.BackgroundImage = bm;
        }

        private void toolStripButton5_MouseEnter(object sender, EventArgs e)
        {
            ToolStripItem tsi = (ToolStripItem)sender;

            Bitmap bm = new Bitmap(tsi.Width, tsi.Height);
            for (int y = 0; y < tsi.Height; y++)
            {
                for (int x = 0; x < tsi.Width; x++)
                    bm.SetPixel(x, y, Color.FromArgb(80, Color.White));
            }

            tsi.BackgroundImage = bm;
        }

        private void toolStripButton4_MouseEnter(object sender, EventArgs e)
        {
            ToolStripItem tsi = (ToolStripItem)sender;

            Bitmap bm = new Bitmap(tsi.Width, tsi.Height);
            for (int y = 0; y < tsi.Height; y++)
            {
                for (int x = 0; x < tsi.Width; x++)
                    bm.SetPixel(x, y, Color.FromArgb(80, Color.White));
            }

            tsi.BackgroundImage = bm;
        }

        private void cutToolStrip_MouseEnter(object sender, EventArgs e)
        {
            ToolStripItem tsi = (ToolStripItem)sender;

            Bitmap bm = new Bitmap(tsi.Width, tsi.Height);
            for (int y = 0; y < tsi.Height; y++)
            {
                for (int x = 0; x < tsi.Width; x++)
                    bm.SetPixel(x, y, Color.FromArgb(80, Color.White));
            }

            tsi.BackgroundImage = bm;
        }

        private void CopyToolStrip_MouseEnter(object sender, EventArgs e)
        {
            ToolStripItem tsi = (ToolStripItem)sender;

            Bitmap bm = new Bitmap(tsi.Width, tsi.Height);
            for (int y = 0; y < tsi.Height; y++)
            {
                for (int x = 0; x < tsi.Width; x++)
                    bm.SetPixel(x, y, Color.FromArgb(80, Color.White));
            }

            tsi.BackgroundImage = bm;
        }

        private void pasteToolStrip_MouseEnter(object sender, EventArgs e)
        {
            ToolStripItem tsi = (ToolStripItem)sender;

            Bitmap bm = new Bitmap(tsi.Width, tsi.Height);
            for (int y = 0; y < tsi.Height; y++)
            {
                for (int x = 0; x < tsi.Width; x++)
                    bm.SetPixel(x, y, Color.FromArgb(80, Color.White));
            }

            tsi.BackgroundImage = bm;
        }

        private void CopyToolStrip_MouseLeave(object sender, EventArgs e)
        {
            (sender as ToolStripItem).BackgroundImage = null;
        }

        private void pasteToolStrip_MouseLeave(object sender, EventArgs e)
        {
            (sender as ToolStripItem).BackgroundImage = null;
        }

        private void cutToolStrip_MouseLeave(object sender, EventArgs e)
        {
            (sender as ToolStripItem).BackgroundImage = null;
        }

        private void toolStripButton4_MouseLeave(object sender, EventArgs e)
        {
            (sender as ToolStripItem).BackgroundImage = null;
        }

        private void toolStripButton5_MouseLeave(object sender, EventArgs e)
        {
            (sender as ToolStripItem).BackgroundImage = null;
        }

        private void moveToTS_MouseLeave(object sender, EventArgs e)
        {
            (sender as ToolStripItem).BackgroundImage = null;
        }

        private void copyToST_MouseLeave(object sender, EventArgs e)
        {
            (sender as ToolStripItem).BackgroundImage = null;
        }

        private void RenameToolStrip_MouseLeave(object sender, EventArgs e)
        {
            (sender as ToolStripItem).BackgroundImage = null;
        }

        private void newFolderToolStrip_MouseLeave(object sender, EventArgs e)
        {
            (sender as ToolStripItem).BackgroundImage = null;
        }

        private void newItemTL_MouseLeave(object sender, EventArgs e)
        {
            (sender as ToolStripItem).BackgroundImage = null;
        }

        private void EasyaccessTS_MouseLeave(object sender, EventArgs e)
        {
            (sender as ToolStripItem).BackgroundImage = null;
        }

        private void propertiesTS_MouseLeave(object sender, EventArgs e)
        {
            (sender as ToolStripItem).BackgroundImage = null;
        }

        private void openTS_MouseLeave(object sender, EventArgs e)
        {
            (sender as ToolStripItem).BackgroundImage = null;
        }

        private void editTS_MouseLeave(object sender, EventArgs e)
        {
            (sender as ToolStripItem).BackgroundImage = null;
        }

        private void hostoryST_MouseLeave(object sender, EventArgs e)
        {
            (sender as ToolStripItem).BackgroundImage = null;
        }

        private void selectAllTS_MouseLeave(object sender, EventArgs e)
        {
            (sender as ToolStripItem).BackgroundImage = null;
        }

        private void selectInvTS_MouseLeave(object sender, EventArgs e)
        {
            (sender as ToolStripItem).BackgroundImage = null;
        }

        private void selectNoneTS_MouseLeave(object sender, EventArgs e)
        {
            (sender as ToolStripItem).BackgroundImage = null;
        }

        private void deleteToolStrip_Click(object sender, EventArgs e)
        {
            deleteToolStripMenuItem_Click(sender, e);
        }

        private void RenameToolStrip_Click(object sender, EventArgs e)
        {
            renameToolStripMenuItem_Click(sender, e);
        }

        private void pasteToolStrip_Click(object sender, EventArgs e)
        {
            pasteToolStripMenuItem1_Click(sender, e);
        }

        private void CopyToolStrip_Click(object sender, EventArgs e)
        {
            copyToolStripMenuItem_Click(sender, e);
        }

        private void cutToolStrip_Click(object sender, EventArgs e)
        {

            cutToolStripMenuItem_Click(sender, e);
        }

        private void deleteToolStripMenuItem_MouseEnter(object sender, EventArgs e)
        {

        }

        private void deleteToolStrip_MouseEnter(object sender, EventArgs e)
        {

            ToolStripItem tsi = (ToolStripItem)sender;

            Bitmap bm = new Bitmap(tsi.Width, tsi.Height);
            for (int y = 0; y < tsi.Height; y++)
            {
                for (int x = 0; x < tsi.Width; x++)
                    bm.SetPixel(x, y, Color.FromArgb(80, Color.White));
            }

            tsi.BackgroundImage = bm;
        }

        private void deleteToolStrip_MouseLeave(object sender, EventArgs e)
        {
            (sender as ToolStripItem).BackgroundImage = null;

        }

        /*----------------------------- List View ----------------------------*/
        private void printListViewItems()
        {
            GUI.addSubFolders(GUI.currentFolder);
            GUI.addSubFiles(GUI.currentFolder);
            
            try
            {
                foreach (Folder subfolder in GUI.currentFolder.getSubFolders())
                {
                    ListViewItem item = new ListViewItem(subfolder.getFolderName());
                    if (subfolder.getId() == 226)
                    {
                        item.ImageIndex = 0;
                    }
                    else if(subfolder.getId() == 228)
                    {
                        item.ImageIndex = 1;
                    }
                    else if(subfolder.getId() == 227)
                    {
                        item.ImageIndex = 3;
                    }
                    else if(subfolder.getId() == 229)
                    {
                        item.ImageIndex = 2;
                    }
                    else if(subfolder.getId() == 230)
                    {
                        item.ImageIndex = 4;
                    }
                    else
                    {
                        item.ImageIndex = 5;
                    }

                    ItemsListView.Items.Add(item);
                }

                foreach (Files file in GUI.currentFolder.getSubFiles())
                {
                    ListViewItem item = new ListViewItem(file.getName());
                    item.ImageIndex = 6;
                    ItemsListView.Items.Add(item);
                }
                ItemsListView.LargeImageList = listViewImgList;
                setPath();
                setTreeView();
                nbrOfItems.Text = ItemsListView.Items.Count.ToString() + " Items";
            }
            catch
            {
            }

        }

        private void setlistViewImgList()
        {
            listViewImgList.Images.Add(Image.FromFile(@"..\..\..\resources\Downloads.png"));
            listViewImgList.Images.Add(Image.FromFile(@"..\..\..\resources\Documents.png"));
            listViewImgList.Images.Add(Image.FromFile(@"..\..\..\resources\Videos.png"));
            listViewImgList.Images.Add(Image.FromFile(@"..\..\..\resources\Musics.png"));
            listViewImgList.Images.Add(Image.FromFile(@"..\..\..\resources\Pictures.png"));
            listViewImgList.Images.Add(Image.FromFile(@"..\..\..\resources\Folder2.png"));
            listViewImgList.Images.Add(Image.FromFile(@"..\..\..\resources\file.png"));
        }

        private void ItemsListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            String selected = ItemsListView.SelectedItems[0].SubItems[0].Text;
            
        }

        private void ItemsListView_ItemActivate(object sender, EventArgs e)
        {

        }

        private void ItemsListView_DoubleClick(object sender, EventArgs e)
        {


        }

        private void ItemsListView_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (ItemsListView.FocusedItem != null)
                {
                    if (ItemsListView.FocusedItem.Bounds.Contains(e.Location))
                    {
                        itemsContextMenu.Show(Cursor.Position);
                    }
                    else if (!ItemsListView.FocusedItem.Bounds.Contains(e.Location))
                    {
                        folderContextMenu.Show(Cursor.Position);
                    }
                }
                else
                {
                    folderContextMenu.Show(Cursor.Position);
                }
            }
        }

        private void ItemsListView_ItemDrag(object sender, ItemDragEventArgs e)
        {
            DoDragDrop(e.Item, DragDropEffects.Copy);

        }

        private void ItemsListView_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.AllowedEffect;
        }

        private void ItemsListView_DragDrop(object sender, DragEventArgs e)
        {
            Folder Destination;
            var pos = ItemsListView.PointToClient(new Point(e.X, e.Y));
            var hit = ItemsListView.HitTest(pos);
            if (hit.Item != null && (Destination = GUI.currentFolder.getSubFolderByName(hit.Item.Text)) != null)
            {
                var draggedItem = (ListViewItem)e.Data.GetData(typeof(ListViewItem));
                Folder Target = GUI.currentFolder.getSubFolderByName(draggedItem.Text);
                if (Target == Destination) return;
                Target.cutFolder(Destination);
                FolderController.cutFolder(Target, Destination);

                deletelistViewItems();
                printListViewItems();
            }
        }

        private void ItemsListView_DragOver(object sender, DragEventArgs e)
        {
            var pos = ItemsListView.PointToClient(new Point(e.X, e.Y));
            ListViewItem destination = ItemsListView.GetItemAt(pos.X, pos.Y);
            if (destination != null)
            {
                destination.Selected = true;
                if (oldItem != null && oldItem != destination)
                    oldItem.Selected = false;
                oldItem = destination;

            }

        }

        private void ItemsListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.IsSelected)
            {
                activateIcons();
            }
            else if (!e.IsSelected)
            {
                deactivateIcons();
            }
        }

        private void deletelistViewItems()
        {
            for (int i = ItemsListView.Items.Count - 1; i >= 0; i--)
            {
                ItemsListView.Items[i].Remove();
            }
        }

        private void ItemsListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                String selected = ItemsListView.SelectedItems[0].SubItems[0].Text;
                GUI.cd(selected);
                deletelistViewItems();
                printListViewItems();
            }
        }

        private void ItemsListView_MouseDown(object sender, MouseEventArgs e)
        {
            ItemsListView_MouseClick(sender, e);
        }

        private void ItemsListView_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            GUI.addSubFolders(GUI.currentFolder);
            ListViewItem item = ItemsListView.Items[e.Item];
            Folder temp = GUI.currentFolder.getSubFolderByName(item.Text);

            try
            {
                if ((String.IsNullOrWhiteSpace(e.Label) || isSpecial(e.Label[0]) == true))
                {
                    MessageBox.Show("Please enter a valid name");

                    if (temp.getSubFolders().Count == 0 && String.IsNullOrWhiteSpace(item.Text))
                    {
                        GUI.deleteSubFolders(temp);
                    }
                    GUI.addSubFolders(GUI.currentFolder);
                    deletelistViewItems();
                    printListViewItems();
                    return;
                }
                else if (GUI.currentFolder.getSubFolders().Find(x => x.getFolderName().ToLower() == e.Label) != null)
                {
                    if (ItemsListView.SelectedItems[0].ImageIndex != 6)
                    {
                        MessageBox.Show("There is already a folder with that name");
                        if (temp.getSubFolders().Count == 0 && String.IsNullOrWhiteSpace(item.Text))
                        {
                            GUI.deleteSubFolders(temp);
                        }
                        GUI.addSubFolders(GUI.currentFolder);
                        deletelistViewItems();
                        printListViewItems();
                        return;
                    }

                }
                else if (fileToBeChanged != null)
                {
                    ListViewItem fileItem = ItemsListView.Items[e.Item];
                    fileToBeChanged.setName(e.Label);
                    FilesController.renameFile(fileToBeChanged);

                    fileToBeChanged = null;
                }
                else
                {
                    temp.rename(e.Label.ToString());
                    FolderController.renameFolder(temp, temp.getFolderName());
                    GUI.addSubFolders(GUI.currentFolder);
                    deletelistViewItems();
                    printListViewItems();

                    folderToBeChanged = null;
                }
                deletelistViewItems();
                printListViewItems();
            }
            catch (Exception)
            {

            }
        }

        /*--------------------------- Tree View ---------------------------*/
        private void insertTreeViewImg()
        {
            treeViewImg.Images.Add(Image.FromFile(@"..\..\..\resources\dcmnts.ico"));
            treeViewImg.Images.Add(Image.FromFile(@"..\..\..\resources\downs.png"));
            treeViewImg.Images.Add(Image.FromFile(@"..\..\..\resources\folder.png"));
            treeViewImg.Images.Add(Image.FromFile(@"..\..\..\resources\musics.ico"));
            treeViewImg.Images.Add(Image.FromFile(@"..\..\..\resources\pics.ico"));
            treeViewImg.Images.Add(Image.FromFile(@"..\..\..\resources\vids.ico"));
            treeViewImg.Images.Add(Image.FromFile(@"..\..\..\resources\user.ico"));
        }

        private void setTreeView()
        {
            foldersTree.Nodes.Clear();
            TreeNode node = new TreeNode(GUI.rootFolder.getFolderName());
            node.Name = GUI.rootFolder.getId() + "";
            node.ImageIndex = 6;
            foldersTree.Nodes.Add(node);
            setSubNodes(GUI.rootFolder, ref node);
            node.Expand();
        }

        private void setSubNodes(Folder currentFolder, ref TreeNode node)
        {
            if(currentFolder.getSubFolders().Any() == true)
            {
                foreach (Folder f in currentFolder.getSubFolders())
                {
                    TreeNode childNode = new TreeNode(f.getFolderName());
                    childNode.Name = f.getId() + "";
                    if(f.getId() == 1 || f.getId() == 2 || f.getId() == 3)
                    {
                        node.Nodes.Add(childNode);
                        childNode.ImageIndex = 6;
                    }
                    if (f.getId() == 226)
                    {
                        node.Nodes.Add(childNode);
                        childNode.ImageIndex = 1;
                    }
                    else if(f.getId() == 228)
                    {
                        node.Nodes.Add(childNode);
                        childNode.ImageIndex = 0;
                    }
                    else if (f.getId() == 227)
                    {
                        node.Nodes.Add(childNode);
                        childNode.ImageIndex = 3;
                    }
                    else if (f.getId() == 229)
                    {
                        node.Nodes.Add(childNode);
                        childNode.ImageIndex = 3;
                    }
                    else if (f.getId() == 230)
                    {
                        node.Nodes.Add(childNode);
                        childNode.ImageIndex = 4;
                    }
                    else
                    {
                        node.Nodes.Add(childNode);
                        childNode.ImageIndex = 2;
                    }
                    childNode.SelectedImageIndex = childNode.ImageIndex;

                    setSubNodes(f, ref childNode);
                }
            }

        }

        private bool isSpecial(char c)
        {
            if(c == '$' || c == '/' || c == '*' || c == '+' || c == '-' || c == '?' || c == '!' || c == '-' || c == '@' || c == '&')
            {
                return true;
            }
            return false;
        }

        private void foldersTree_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {

        }

        private void foldersTree_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            foldersTree.SelectedNode.BackColor = Color.Aqua;
            foldersTree.SelectedNode.ForeColor = Color.White;
            previousSelectedNode = foldersTree.SelectedNode;
        }

        private void foldersTree_Enter(object sender, EventArgs e)
        {
            if (foldersTree.SelectedNode != null)
            {
                foldersTree.SelectedNode.BackColor = Color.Empty;
                foldersTree.SelectedNode.ForeColor = Color.Empty;
            }
        }

        private void foldersTree_Leave(object sender, EventArgs e)
        {
            if (foldersTree.SelectedNode != null)
            {
                foldersTree.SelectedNode.BackColor = SystemColors.Highlight;
                foldersTree.SelectedNode.ForeColor = Color.White;
            }
        }
        private void foldersTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            var item = foldersTree.SelectedNode;
            if (item != null)
            {
                if (item.Text.ToLower() == GUI.rootFolder.getFolderName().ToLower())
                {
                    GUI.currentFolder = GUI.rootFolder;
                }
                else
                {
                    GUI.currentFolder = GUI.rootFolder.getSubFolderById(GUI.rootFolder, int.Parse(item.Name));
                }
                GUI.path = item.FullPath.ToString();
                deletelistViewItems();
                printListViewItems();
                setPath();
            }
            if (previousSelectedNode != null)
            {
                previousSelectedNode.BackColor = foldersTree.BackColor;
                previousSelectedNode.ForeColor = foldersTree.ForeColor;
            }
        }

        public static void SetTreeViewTheme(IntPtr treeHandle)
        {
            SetWindowTheme(treeHandle, "explorer", null);
        }

        [DllImport("uxtheme.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
        private static extern int SetWindowTheme(IntPtr hwnd, string pszSubAppName, string pszSubIdList);

        /*------------------------  Context Menus --------------------*/
        private void folderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GUI.mkdir("");
            ListViewItem item = new ListViewItem("");
            item.ImageIndex = 5;
            ItemsListView.Items.Add(item);
            ItemsListView.LabelEdit = true;
            item.BeginEdit();

        }

        private void renameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ItemsListView.SelectedItems[0].ImageIndex == 6)
            {
                String selected = ItemsListView.SelectedItems[0].SubItems[0].Text;
                fileToBeChanged = GUI.currentFolder.getSubFile(selected);
                folderToBeChanged = null;
                ItemsListView.LabelEdit = true;
                ItemsListView.SelectedItems[0].BeginEdit();

            }
            else
            {
                fileToBeChanged = null;
                folderToBeChanged = ItemsListView.SelectedItems[0].SubItems[0].Text;
                ItemsListView.LabelEdit = true;
                ItemsListView.SelectedItems[0].BeginEdit();
            }


        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(ItemsListView.SelectedItems[0].ImageIndex == 6)
            {
                String selected = ItemsListView.SelectedItems[0].SubItems[0].Text;
                cutFile = GUI.currentFolder.getSubFile(selected);
                copiedFile = null;
                toBeCopied = null;
                toBeCut = null;
                ativatePasteIcons();
                cutToolStrip_MouseLeave(sender, e);
            
            }
            else
            {
                String selected = ItemsListView.SelectedItems[0].SubItems[0].Text;
                toBeCut = GUI.currentFolder.getSubFolderByName(selected);
                toBeCopied = null;
                copiedFile = null;
                cutFile = null;
                ativatePasteIcons();
                cutToolStrip_MouseLeave(sender, e);
            }
            
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ItemsListView.SelectedItems[0].ImageIndex == 6)
            {
                String selected = ItemsListView.SelectedItems[0].SubItems[0].Text;
                Files tmp = GUI.currentFolder.getSubFile(selected);

                FilesController.deleteFileById(tmp.getId());
                cutToolStrip_MouseLeave(sender, e);
                deletelistViewItems();
                printListViewItems();

            }
            else
            {
                String selected = ItemsListView.SelectedItems[0].SubItems[0].Text;

                Folder tmp = GUI.currentFolder.getSubFolderByName(selected);
                try
                {
                    if ((toBeCut != null && toBeCopied != null) && (tmp.getId() == toBeCopied.getId() || tmp.getId() == toBeCut.getId()))
                    {
                        toBeCut = null;
                        toBeCopied = null;
                        deactivatePasteIcons();
                    }
                    GUI.deleteSubFolders(tmp);
                    deletelistViewItems();
                    printListViewItems();
                    deleteToolStrip_MouseLeave(sender, e);
                }
                catch (Exception exc)
                {

                }
            }
        }

        private void pasteToolStripMenuItem1_Click(object sender, EventArgs e)
        {

            if (toBeCopied != null)
            {
                if (GUI.currentFolder.getParentFolder() == toBeCopied.getParentFolder()
                    && ItemsListView.Items.ToString().Contains(GUI.currentFolder.getFolderName())
                    && ItemsListView.Items.ToString().Contains(toBeCut.getParentFolder().getFolderName()))
                { 
                        toBeCopied = null;
                        deactivatePasteIcons();
                        return;
                }
                else if (GUI.currentFolder.getSubFolders().Find(x => x.getFolderName().ToLower() == toBeCopied.getFolderName()) != null)
                {
                    MessageBox.Show("There is already a folder with that name");
                    toBeCopied = null;
                    deactivatePasteIcons();
                    return;
                }
                else
                {
                    if(GUI.currentFolder.getParentFolder() !=  toBeCopied)
                        GUI.copyFolder(toBeCopied, GUI.currentFolder);
                }
            }
            else if (copiedFile != null)
            {
                if (GUI.currentFolder.getParentFolder() != null && GUI.currentFolder.getParentFolder().getId() == copiedFile.getFolderId())
                {
                    deactivatePasteIcons();
                    return;
                }
                else if(GUI.currentFolder.getSubFiles().Find(x => x.getName().ToLower() == copiedFile.getName()) != null){
                    MessageBox.Show("There is already a file with that name");
                    copiedFile = null;
                    deactivatePasteIcons();
                    return;
                }
                else
                {
                    FilesController.copyFile(copiedFile,GUI.currentFolder);
                }
            }
            else if (cutFile != null)
            {
                if (GUI.currentFolder.getParentFolder() != null && GUI.currentFolder.getParentFolder().getId() == copiedFile.getFolderId())
                {
                    deactivatePasteIcons();
                    return;
                }
                else if (GUI.currentFolder.getSubFiles().Find(x => x.getName().ToLower() == cutFile.getName()) != null)
                {
                    MessageBox.Show("There is already a file with that name");
                    cutFile = null;
                    deactivatePasteIcons();
                    return;
                }
                else
                {
                    FilesController.cutFile(cutFile.getId(), GUI.currentFolder.getId());
                }

            }
            else if(toBeCut != null)
            {

                if (GUI.currentFolder.getSubFolders().Find(x => x.getFolderName().ToLower() == toBeCut.getFolderName()) != null)
                {
                    MessageBox.Show("There is already a folder with that name");
                    toBeCut = null;
                    deactivatePasteIcons();
                    return;
                }

                else if(GUI.currentFolder.getParentFolder() == toBeCut.getParentFolder())
                {
                    if(ItemsListView.Items.ToString().Contains(GUI.currentFolder.getFolderName()) && ItemsListView.Items.ToString().Contains(toBeCut.getParentFolder().getFolderName()))
                    {
                        toBeCut = null;
                        deactivatePasteIcons();
                        return;
                    }
                    else
                    {
                        toBeCut.cutFolder(GUI.currentFolder);
                        FolderController.cutFolder(toBeCut, GUI.currentFolder);
                    }

                }
                else
                {
                    toBeCut.cutFolder(GUI.currentFolder);
                    FolderController.cutFolder(toBeCut, GUI.currentFolder);
                }
            }
            deletelistViewItems();
            printListViewItems();
            toBeCopied = null;
            toBeCut = null;
            pasteToolStrip_MouseLeave(sender, e);
            deactivatePasteIcons();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ItemsListView.SelectedItems[0].ImageIndex == 6)
            {
                String selected = ItemsListView.SelectedItems[0].SubItems[0].Text;
                copiedFile = GUI.currentFolder.getSubFile(selected);
                
            }
            else
            {
                String selected = ItemsListView.SelectedItems[0].SubItems[0].Text;
                toBeCopied = GUI.currentFolder.getSubFolderByName(selected);
                toBeCut = null;
            }
            ativatePasteIcons();
            CopyToolStrip_MouseLeave(sender, e);

        }

    }
}
