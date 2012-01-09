var numberOfGoalsToHit = 1;
var world : World;

function updateGoals()
{
	numberOfGoalsToHit -= 1;
	
	if (numberOfGoalsToHit == 0)
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