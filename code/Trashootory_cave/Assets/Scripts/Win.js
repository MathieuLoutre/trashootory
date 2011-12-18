var numberOfGoalsToHit = 1;
var numberOfTouchedGoals = 0;
var lift : Elevator;

function updateGoals()
{
	numberOfTouchedGoals += 1;
	
	if (numberOfTouchedGoals == numberOfGoalsToHit)
	{
		lift.NextLevel();
	}
}