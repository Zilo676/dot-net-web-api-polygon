namespace SberTestProject;

class Program
{
    static void Main(string[] args)
    {
        var root = new Node(1);
        var root2 = new Node(2);
        var root3 = new Node(3);
        
        root.Left = root2;
        root.Right = root3;
        
        var root4 = new Node(4);
        
        root.Left.Left = root4;
        var root5 = new Node(5);
        var root6 = new Node(6);
        root.Right.Left = root5;
        root.Right.Right = root6;

        Console.WriteLine(TreeHelper.TreeToString(root));
        ;

        var newRoot = TreeHelper.Swap(root);
        Console.WriteLine(TreeHelper.TreeToString(newRoot));
        ;
    }
}

public static class TreeHelper

{
    public static Node Swap(Node tree)

    {
        if (tree == null) return null;
        (tree.Left, tree.Right) = (tree.Right, tree.Left);
        Swap(tree.Left);
        Swap(tree.Right);
        return tree;
    }
    
    public static string TreeToString(Node root)
    {
        if (root == null) return "null";
    
        var result = new List<string>();
        Preorder(root, result);
        return string.Join(", ", result);
    }

    private static void Preorder(Node node, List<string> result)
    {
        if (node == null)
        {
            result.Add("null");
            return;
        }
    
        result.Add(node.Value.ToString());
        Preorder(node.Left, result);
        Preorder(node.Right, result);
    }
}

public class Node

{
    public int Value { get; set; }

    public Node Left { get; set; }

    public Node Right { get; set; }

    public Node(int value)

    {
        Value = value;
    }

    public override string ToString() => Value.ToString();
}