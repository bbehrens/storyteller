using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using FubuCore.Util;
using StoryTeller.Model;

namespace StoryTeller.Workspace
{
    public class StartupAction
    {
        [XmlAttribute]
        public string Name { get; set; }
    }

    [Serializable]
    public class WorkspaceFilter
    {
        private readonly IList<FixtureFilter> _filters = new List<FixtureFilter>();

        public WorkspaceFilter(IEnumerable<WorkspaceFilter> workspaceFilters) : this()
        {
            _filters.AddRange(workspaceFilters.SelectMany(x => x.Filters));
        }

        public WorkspaceFilter()
        {
            StartupActions = new string[0];
        }

        // Only here to make the XMl prettier
        public StartupAction[] Actions
        {
            get
            {
                return (StartupActions ?? new string[0]).Select(x => new StartupAction() {Name = x}).ToArray();
            }
            set
            {
                StartupActions = value.Select(x => x.Name).ToArray();
            }
        }

        [XmlIgnore]
        public string[] StartupActions { get; set; }

        [XmlAttribute]
        public string Name { get; set; }
        public FixtureFilter[] Filters
        {
            get
            {
                return _filters.Any() ? _filters.ToArray() : new FixtureFilter[]{FixtureFilter.All()};
            }
            set
            {
                _filters.Clear();
                _filters.AddRange(value);
            }
        }

        public int FilterCount
        {
            get { return _filters.Count; }
        }

        public void AddFilter(FixtureFilter filter)
        {
            _filters.Add(filter);
        }

        public CompositeFilter<Type> CreateTypeFilter()
        {
            var filter = new CompositeFilter<Type>();

            _filters.Each(x => x.Apply(filter));

            return filter;
        }

        public CompositeFilter<FixtureGraph> CreateFixtureFilter()
        {
            var filter = new CompositeFilter<FixtureGraph>();

            _filters.Each(x => x.Apply(filter));

            return filter;
        }
    }
}