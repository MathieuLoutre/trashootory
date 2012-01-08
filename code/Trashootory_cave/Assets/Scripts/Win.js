var numberOfGoalsToHit = 1;
var numberOfTouchedGoals = 0;
var world : World;

function updateGoals()
{
	numberOfTouchedGoals += 1;
	
	if (numberOfTouchedGoals == numberOfGoalsToHit)
	{
		world.NextLevel();
	}
}