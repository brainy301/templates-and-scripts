var API_URL = "http://localhost:5014/api/";

$('#myTab a').on('click', function (e) {
  e.preventDefault()
  $(this).tab('show')
})

// Load
function RequestFiles()
{
	var jqxhr = $.ajax(API_URL + "items/getfiles" 
	/*, 
		method     : 'get',		
		crossDomain: true,
		contentType: 'application/json',
		dataType   : 'json',
		processData: false
		}*/
		)
	  .done(function(data) {
		console.log("data", data);
				
		var $filesDropdownMenu = $('#Files .dropdown-menu');
		$filesDropdownMenu.empty();
		for (var i=0; i<data.length; i++)
		{
			$filesDropdownMenu.append($('<a class="dropdown-item" href="#">' + data[i].name + '</a>'));
		}
		
		// Change drop down selection
		$("#Files .dropdown-menu a").off('click').on('click', function(){
			console.log("click ", $(this).text());
		  $("#Files .btn:first-child").text($(this).text());
		  $("#Files .btn:first-child").val($(this).text());
		});			
		
	  })
	  .fail(function() {
		alert( "error getting files" );
	  });
}

function GetFilename(){
	var filename = $("#Files .btn:first-child").val();
	return filename;
}

function GetPassword() {
	return $("#filepassword").val();
}

var openTasks = [];
var openTasksLoaded = false;
function GetOpenTasks(reload, reloadfile) {
	
	if (reload)
		openTasksLoaded = false;
	if (openTasksLoaded)
		return;
		
	var filename = GetFilename();
	var password = GetPassword();
	//console.log("details", filename, password);
	
	$.ajax(API_URL + "items/" + (reloadfile? 're' : '') + "loadfile?name=" + filename + "&password=" + password)
	  .done(function(data) {
		if (data == false)
		{
			alert("error loading file. Reports false");
			return;
		}
		$.ajax(API_URL + "tasks/getopentasks?filename=" + filename)
			.done(function(tasksdata) {
				openTasks = tasksdata;
				var $opentasks = $('#opentasks #tasks');
				$opentasks.empty();
				for (var i=0; i<tasksdata.length; i++) {
					var task = tasksdata[i];
					$opentasks.append('<div id="' + task.id + '" class="task row"><div class="col-md-11 tasktext">' + task.html + '</div><div class="col-md-1"><i class="fa fa-check" aria-hidden="true"></i>&nbsp;<i class="fa fa-trash-o" aria-hidden="true"></i></div></div>')
					for (var j=0; j<task.childItems.length; j++) {
						var childItem = task.childItems[j];
						$opentasks.append('<div id="' + childItem.id + '" class="task row"><div class="col-md-11 subtasktext tasktext">' + childItem.html + '</div><div class="col-md-1"><i class="fa fa-check" aria-hidden="true"></i>&nbsp;<i class="fa fa-trash-o" aria-hidden="true"></i></div></div>')
					}
				}
				$opentasks.find('.fa-trash-o').click(OnDeleteTask);
				$opentasks.find('.fa-check').click(OnCompleteTask);		
				$opentasks.find('.tasktext').click(OnTaskClick);		
				
				console.log("tasks data", tasksdata);
			})
			.fail(function() {
				alert("failed to load task data");
			});
	  })
	  .fail(function() {
		alert( "error loading file" );
	  });	  
}

var closedTasks = [];
var closedTasksLoaded = false;
function GetClosedTasks(reload, reloadfile) {
	
	if (reload)
		closedTasksLoaded = false;
	if (closedTasksLoaded)
		return;
		
	var filename = GetFilename();
	var password = GetPassword();
	$.ajax(API_URL + "items/" + (reloadfile? 're' : '') + "loadfile?name=" + filename + "&password=" + password)
	  .done(function(data) {
		if (data == false)
		{
			alert("error loading file. Reports false");
			return;
		}
		$.ajax(API_URL + "tasks/getclosedtasks?filename=" + filename)
			.done(function(taskgroups) {
				closedTasks = taskgroups;
				var $closedtasks = $('#closedtasks #tasks');
				$closedtasks.empty();							
				
				for (var i=0; i<taskgroups.length; i++) {
					var taskgroup = taskgroups[i];
					if (i>0)
						$closedtasks.append('<div class="completedDateEnd"></div>');						
					$closedtasks.append('<div class="row completedDate"><div class="col-md-12">' + taskgroup.date + ' (' + taskgroup.completedCount + ')</div></div>');
					for (var j=0; j<taskgroup.tasks.length; j++) {
						var task = taskgroup.tasks[j];
						$closedtasks.append('<div id="' + task.id + '" class="task row' + (task.completedOn == null ? ' notcomplete' : '') + '"><div class="tasktext col-md-11">' + task.html + '</div><div class="col-md-1"><i class="fa fa-trash-o" aria-hidden="true"></i></div></div>')
						for (var k=0; k<task.childItems.length; k++) {
							var childItem = task.childItems[k];
							$closedtasks.append('<div id="' + childItem.id + '" class="task row"><div class="col-md-11 subtasktext tasktext">' + childItem.html + '</div><div class="col-md-1"><i class="fa fa-trash-o" aria-hidden="true"></i></div></div>')
						}						
					}					
				}
													
				$closedtasks.find('.fa-trash-o').click(OnDeleteTaskClosed);
				$closedtasks.find('.tasktext').click(OnTaskClick);
				console.log("tasks data", taskgroups);
			})
			.fail(function() {
				alert("failed to load task data");
			});
	  })
	  .fail(function() {
		alert( "error loading file" );
	  });	  
}

function OnDeleteTask() {
	var $task = $(this).parents('.task:first');
	var id = $task.attr('id');
	$.ajax(API_URL + "items/deleteitem?name=" + GetFilename() + "&itemid=" + id)
	  .done(function(data) {	
		GetOpenTasks(true);
	  }).fail(function() {
		alert( "error deleting task" );
	  });	
}

function OnDeleteTaskClosed() {
	var $task = $(this).parents('.task:first');
	var id = $task.attr('id');
	$.ajax(API_URL + "items/deleteitem?name=" + GetFilename() + "&itemid=" + id)
	  .done(function(data) {	
		GetClosedTasks(true);
	  }).fail(function() {
		alert( "error deleting task" );
	  });	
}

function OnCompleteTask() {
	var $task = $(this).parents('.task:first');
	var id = $task.attr('id');
	$.ajax(API_URL + "tasks/completetask?filename=" + GetFilename() + "&itemid=" + id)
	  .done(function(data) {	
		GetOpenTasks(true);
	  }).fail(function() {
		alert( "error completing task" );
	  });	
}

function OnTaskClick() {
	var $task = $(this).parents('.task:first');
	var id = $task.attr('id');
	$('#taskModal').modal('show');
}

function OnAddTask(doNotClearParent) {
	var taskText = $('#addtasktext').val();
	var parentId = $('#parenttaskid').val();
	$.ajax(API_URL + "tasks/addtask?filename=" + GetFilename() + "&task=" + taskText + "&parentid=" + parentId)
	  .done(function(data) {	
		GetOpenTasks(true);
		$('#addtasktext').val('');
		if (!doNotClearParent)
			$('#parenttaskid').val('');
	  }).fail(function() {
		alert( "error adding task" );
	  });
}

function OnSaveFile() {	
	$.ajax(API_URL + "items/savefile?name=" + GetFilename() + "&password=" + GetPassword())
	  .done(function(data) {	
		if (data == false) {
			alert( "error saving file. Return false" );
			return;
		}
		alert("Saved successfully");
	  }).fail(function() {
		alert( "error saving file" );
	  });
}

var VIEW_OPEN_TASKS = 0;
var VIEW_CLOSED_TASKS = 1;
var currentView = VIEW_OPEN_TASKS;

function OnTabShow() {
	$('#taskstab a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
		var $tab = $(e.target);
		if ($tab.attr('id') == 'closedtasks-tab')
		{
			console.log('showing closed tasks')
			GetClosedTasks(true);
			currentView = VIEW_CLOSED_TASKS;
			
		} else {
			currentView = VIEW_OPEN_TASKS;
		}
		console.log("tab show", e.target, e.relatedTarget);
	});
}

$(function() {
	$('#btnAddTask').click(OnAddTask);
	$('#btnAddSameParentTask').click(function() { OnAddTask(true); });	
	$('#btnSaveFile').click(OnSaveFile);
	$('#btnLoadFile').click(function() { GetOpenTasks(true); });
	$('#btnReloadFile').click(function() { GetOpenTasks(true, true); });
	OnTabShow();
	RequestFiles();
});




