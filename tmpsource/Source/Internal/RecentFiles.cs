using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AcornPad.Internal
{
    public class RecentFilesList
    {
        /// <summary>
        /// Maximum files to remember
        /// </summary>
        private const int MAXCOUNT = 10;

        /// <summary>
        /// Pointer to RecentFilesMenu toolstrip
        /// </summary>
        private readonly ToolStripMenuItem RecentFilesMenu;

        /// <summary>
        /// Get list of  files (max 10)
        /// </summary>
        private readonly List<string> ProjectList = new List<string>();

        /// <summary>
        /// Action Delegate to OpenProject(string filename) function
        /// </summary>
        public Action<string> OpenProject { get; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="menu"></param>
        /// <param name="action"></param>
        public RecentFilesList(ToolStripMenuItem menu, Action<string> action)
        {
            RecentFilesMenu = menu;
            OpenProject = action;
            UpdateMenu();
        }

        /// <summary>
        ///
        /// </summary>
        private void UpdateMenu()
        {
            RecentFilesMenu.DropDownItems.Clear();

            ToolStripMenuItem menuItm = new ToolStripMenuItem();

            for (int i = 0; i <= ProjectList.Count - 1; i++)
            {
                menuItm = new ToolStripMenuItem
                {
                    Text = (i + 1).ToString() + " " + ProjectList[i],
                    Tag = ProjectList[i]
                };

                menuItm.Click += MenuItm_Click;
                RecentFilesMenu.DropDownItems.Add(menuItm);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuItm_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            OpenProject(item.Tag.ToString());
        }

        /// <summary>
        /// Add new filename to the list
        /// </summary>
        /// <param name="filename"></param>
        public void Add(string filename)
        {
            while (ProjectList.Contains(filename))
            {
                ProjectList.Remove(filename);
            }

            ProjectList.Insert(0, filename);

            while (ProjectList.Count > MAXCOUNT)
            {
                ProjectList.RemoveAt(ProjectList.Count - 1);
            }

            UpdateMenu();
        }
    }
}