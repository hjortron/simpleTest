using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleTest
{
    internal class BlackListNode : IEnumerable<BlackListNode>
    {
        private Dictionary<string, BlackListNode> _childDomains = new Dictionary<string, BlackListNode>();

        public readonly string DomainLabel;

        public BlackListNode ParentDomain { get; private set; }
           
        public BlackListNode(string domainLabel)
        {
            DomainLabel = domainLabel;
        }

        public BlackListNode GetChild(string domainLabel)
        {
            return _childDomains.ContainsKey(domainLabel) ? _childDomains[domainLabel] : null;
        }

        private void Add(string[] array)
        {
            if (_childDomains == null)
            {
                return;
            }

            if (array.Length == 0)
            {
                _childDomains = null;
                return;
            }

            BlackListNode item;
            if (!_childDomains.ContainsKey(array.First())) 
            {
                item = new BlackListNode(array.First()) {ParentDomain = this};
                _childDomains.Add(item.DomainLabel, item);               
            }
            else
            {
                item = GetChild(array.First());              
            }

            item.Add(array.Skip(1).ToArray());      
        }
     
        public int Count
        {
            get { return _childDomains == null ? 0 : _childDomains.Count; }
        }

        public void AddFullDomainName(string fullDomainName)
        {
            Add(fullDomainName.Split('.').Reverse().ToArray());           
        }

        public void AddFqdnArray(string[] labelsArray)
        {           
           Add(labelsArray);
        }

        private bool ContainsNodes(string[] nodes)
        {         
            if (_childDomains == null )
            {
                return true;
            }

            if (nodes.Length == 0 || !_childDomains.ContainsKey(nodes.First()))
            {                
                return false;
            }
               
            var item = GetChild(nodes.First());
            
            return item.ContainsNodes(nodes.Skip(1).ToArray());      
        }

        public bool ContainsUrl(string url)
        {
            var decomposedUrl = url.Split('.').Reverse().ToArray();
            return ContainsNodes(decomposedUrl);
        }   

        public IEnumerator<BlackListNode> GetEnumerator()
        {
            return _childDomains.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}

