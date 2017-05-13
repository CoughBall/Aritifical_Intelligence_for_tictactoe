using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TreeNode : IEnumerable<TreeNode>
{
    private readonly Dictionary<string, TreeNode> _children = new Dictionary<string, TreeNode>();

    public readonly string ID; // X or circle
    public TreeNode Parent { get; private set; }

    public TreeNode(string id)
    {
        this.ID = id;
    }

    public TreeNode GetChild(string id)
    {
        return this._children[id];
    }

    public void Add(TreeNode item)
    {
        if (item.Parent != null)
        {
            item.Parent._children.Remove(item.ID);
        }

        item.Parent = this;
        this._children.Add(item.ID, item);
    }

    public IEnumerator<TreeNode> GetEnumerator()
    {
        return this._children.Values.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }

    public int Count
    {
        get { return this._children.Count; }
    }


    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
