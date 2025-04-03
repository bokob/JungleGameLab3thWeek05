using System.Collections.Generic;
using static Define;

public class Node
{
    protected NodeState nodeState;

    // 부모, 자식 노드
    // 자식을 보고 합성 노드를 만들고, 부모를 보고 공유 데이터 접근 가능하게 한다.
    protected Node parent;
    public Node Parent => parent;
    protected List<Node> children = new List<Node>();
    //public Node Parent => parent;

    // 공유 데이터를 처리하기 위한 딕셔너리
    Dictionary<string, object> _dataContext = new Dictionary<string, object>();

    #region 생성자
    public Node()
    {
        parent = null;
    }

    public Node(List<Node> children)
    {
        foreach(Node child in children)
            AddChild(child);
    }
    #endregion

    // 자식 노드 추가
    void AddChild(Node node)
    {
        node.parent = this;
        children.Add(node);
    }

    // 평가 함수
    // 각 파생 노드 클래스에서 자체 평가 함수를 구현하고 고유한 역할을 수행하여 노드 클래스를 마무리한다.
    public virtual NodeState Evaluate() => NodeState.Failure;

    // 데이터 설정
    public void SetData(string key, object value)
    {
        _dataContext[key] = value;
    }

    // 데이터 반환
    public object GetData(string key)
    {
        object value = null;
        if(_dataContext.TryGetValue(key, out value))
            return value;

        // 재귀적으로 거슬러 올라가면서 확인
        Node node = parent;
        while(node != null)
        {
            value = node.GetData(key);
            if (value != null)
                return value;
            node = node.parent;
        }
        return null;
    }

    // 공유 데이터 지우기
    public bool ClearData(string key)
    {
        if(_dataContext.ContainsKey(key))
        {
            _dataContext.Remove(key);
            return true;
        }

        // 재귀적으로 거슬러 올라가면서 해당하는 정보 삭제
        Node node = parent;
        while(node != null)
        {
            bool cleared = node.ClearData(key);
            if(cleared)
                return true;
            node = node.parent;
        }
        return false;
    }
}