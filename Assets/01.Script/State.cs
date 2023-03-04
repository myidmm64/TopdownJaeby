
public abstract class State<T>
{
	protected StateMachine<T> _stateMachine;
	protected T _stateMachineOwnerClass; // stateMachine을 갖고 있는 클래스를 가리킴
	public State() { }
	// internal : 같은 프로젝트? 내에서만 쓸수있게 제한? 머 같은 어셈블리 내에서 제한 머시깽이같음
	internal void SetMachineWithClass(StateMachine<T> stateMachine, T stateMachineClass)
	{
		this._stateMachine = stateMachine;
		this._stateMachineOwnerClass = stateMachineClass;

		OnAwake();
	}

	/// <summary>
	/// 해당 상태를 넣을 때 초기화 할 때 호출
	/// </summary>
	public virtual void OnAwake() { }
	/// <summary>
	/// 해당 상태를 시작할 때 1회 호출
	/// </summary>
	public abstract void Enter();

	/// <summary>
	/// 해당 상태를 업데이트할 때 매 프레임 호출
	/// </summary>
	public abstract void Execute();

	/// <summary>
	/// 해당 상태를 종료할 때 1회 호출
	/// </summary>
	public abstract void Exit();
}