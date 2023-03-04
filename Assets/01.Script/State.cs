
public abstract class State<T>
{
	protected StateMachine<T> _stateMachine;
	protected T _stateMachineOwnerClass; // stateMachine�� ���� �ִ� Ŭ������ ����Ŵ
	public State() { }
	// internal : ���� ������Ʈ? �������� �����ְ� ����? �� ���� ����� ������ ���� �ӽò��̰���
	internal void SetMachineWithClass(StateMachine<T> stateMachine, T stateMachineClass)
	{
		this._stateMachine = stateMachine;
		this._stateMachineOwnerClass = stateMachineClass;

		OnAwake();
	}

	/// <summary>
	/// �ش� ���¸� ���� �� �ʱ�ȭ �� �� ȣ��
	/// </summary>
	public virtual void OnAwake() { }
	/// <summary>
	/// �ش� ���¸� ������ �� 1ȸ ȣ��
	/// </summary>
	public abstract void Enter();

	/// <summary>
	/// �ش� ���¸� ������Ʈ�� �� �� ������ ȣ��
	/// </summary>
	public abstract void Execute();

	/// <summary>
	/// �ش� ���¸� ������ �� 1ȸ ȣ��
	/// </summary>
	public abstract void Exit();
}