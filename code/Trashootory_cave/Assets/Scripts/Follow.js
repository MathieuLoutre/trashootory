
function Update () {
	var screenSpace = Camera.main.WorldToScreenPoint(transform.position);
	    var offset = transform.position - Camera.main.ScreenToWorldPoint(Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z));
	   var curScreenSpace = Vector3(Input.mousePosition.x, Input.mousePosition.y, screenSpace.z);
       var curPosition = Camera.main.ScreenToWorldPoint(curScreenSpace) + offset;
       transform.position = curPosition;
	}