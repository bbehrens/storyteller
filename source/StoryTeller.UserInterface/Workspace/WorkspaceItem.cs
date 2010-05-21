﻿using System;
using System.Windows.Controls;

namespace StoryTeller.UserInterface.Workspace
{
    public class WorkspaceItem : StackPanel
    {
        private readonly CheckBox _checkbox;
        private readonly string _name;

        public WorkspaceItem(string name, bool selected)
        {
            this.Horizontal();
            _checkbox = this.Add<CheckBox>();
            this.Add<Label>(l => l.Content = name);
            _name = name;

            _checkbox.IsChecked = selected;
        }

        public string WorkspaceName
        {
            get { return _name; }
        }

        public bool Selected
        {
            get
            {
                return _checkbox.IsChecked.GetValueOrDefault(false);
            }
            set { _checkbox.IsChecked = value; }
        }
    }
}