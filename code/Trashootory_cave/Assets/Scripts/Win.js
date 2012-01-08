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

// for debugging purposes: type N to go to the next level
function Update () 
{
	if (Input.GetKeyDown(KeyCode.N))
		world.NextLevel();
}