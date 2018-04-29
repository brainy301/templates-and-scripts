Test:
	Items:
		http://localhost:5014/api/items/ping
		http://localhost:5014/api/items/currentdirectory	
		http://localhost:5014/api/items/getfiles
		http://localhost:5014/api/items/getallitems?name=Test Tasks.itf
		http://localhost:5014/api/items/loadfile?name=Test Tasks.itf&password=
		http://localhost:5014/api/items/loadfile?name=Test Tasks Encrypted.itf&password=
		http://localhost:5014/api/items/loadfile?name=Test Tasks Encrypted.itf&password=123
		http://localhost:5014/api/items/savefile?name=Test Tasks.itf
		http://localhost:5014/api/items/savefile?name=Test Tasks Encrypted.itf&password=123
		http://localhost:5014/api/items/deleteitem?name=Test Tasks.itf&itemid=324ed742-dc40-4dc9-8777-348cb9687ec2
	Tasks:
		http://localhost:5014/api/tasks/ping
		http://localhost:5014/api/tasks/getalltasks?filename=Test Tasks.itf
		http://localhost:5014/api/tasks/getopentasks?filename=Test Tasks.itf
		http://localhost:5014/api/tasks/getclosedtasks?filename=Test Tasks.itf
		http://localhost:5014/api/tasks/addtask?filename=Test Tasks.itf&task=This is a test task
		http://localhost:5014/api/tasks/completetask?filename=Test Tasks.itf&itemid=...
		

	Test Tasks Encrypted

	