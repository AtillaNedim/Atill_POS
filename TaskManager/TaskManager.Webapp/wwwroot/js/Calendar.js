document.addEventListener('DOMContentLoaded', function() {
    var calendarEl = document.getElementById('calendar');
    var calendar = new FullCalendar.Calendar(calendarEl, {
        initialView: 'listWeek',
        headerToolbar: {
            start: '', 
            center: '', 
            end: ''
        },
        events: [
            {
                title: 'Birthday Party',
                start: '2019-08-13T07:00:00',
                backgroundColor: 'green',
                borderColor: 'green'
            },
            {
                title: 'Project Deadline',
                start: '2024-06-15',
                end: '2024-06-15'
            },
            {
                title: 'Conference',
                start: '2024-07-20',
                end: '2024-07-22'
            },
            {
                title: 'Meeting',
                start: '2019-08-12T14:30:00',
                extendedProps: {
                    status: 'done'
                }
            },
        ],
        contentHeight: 'auto'
    });
    calendar.render();
});
console.log('Testing if Calendar.js is executed.');
