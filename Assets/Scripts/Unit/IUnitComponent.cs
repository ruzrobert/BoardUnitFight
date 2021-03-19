public interface IUnitComponent
{
	Unit Unit { get; }

	void Setup(Unit unit);
}