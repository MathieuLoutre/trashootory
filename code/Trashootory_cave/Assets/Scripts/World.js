var NumberOfPellets = 5;
var NumberOfLevels = 5;
var lift : Elevator;

function End()
{
	if (NumberOfLevels == 0 || NumberOfPellets == 0)
	{
		if (NumberOfPellets == 0 && NumberOfLevels > 0)
		{
			var i = NumberOfLevels;
			
			for (i = NumberOfLevels; i > 0; i = i-1)
			{
				lift.NextLevel();
				yield WaitForSeconds(12.0);
			}
		}
	}
}

function PelletThrown()
{
	NumberOfPellets -= 1;	
	End();
}

function NextLevel()
{
	NumberOfLevels -= 1;
	lift.NextLevel();
	End();
}