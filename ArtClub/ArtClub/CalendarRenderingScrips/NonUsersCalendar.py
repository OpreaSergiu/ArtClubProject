import sys, getopt
import pypyodbc 
import datetime

from datetime import *
from datetime import timedelta
from calendar import monthrange


connection = pypyodbc.connect("Driver={SQL Server Native Client 11.0}; Server=(localdb)\MSSQLLocalDB; Database=aspnet-ArtClub-20180506023021; Trusted_Connection=yes;") 
cursor = connection.cursor() 

SQLCommand = """SELECT FORMAT(Date,'yyyy-MM-dd') FROM ReservationsModels WHERE LocationReserved = '%s' AND FORMAT(Date,'yyyy-MM') = '%s' ORDER BY Date"""

if len(sys.argv) > 1:
	location_id = sys.argv[1]
	reservation_month = sys.argv[2]
else:
	location_id = ""
	reservation_month = ""

cursor.execute(SQLCommand % (location_id,reservation_month)) 

reservated_dates = []

results = cursor.fetchone() 

while results:
	reservated_dates.append(results[0])
	results = cursor.fetchone() 

year = int(reservation_month.split('-')[0])
month =  int(reservation_month.split('-')[1])
dt = datetime(year, month, 1)

now = dt.strftime("%m")

first = dt

months = int(now)
months = months + 1

next_month = dt.replace(month=months).replace(day=1)

last = next_month - timedelta(days=1)

result_list = '<table class="table table-striped table-bordered"><tr><th>Mo</th><th>Tu</th><th>We</th><th>Th</th><th>Fr</th><th>Sa</th><th>Su</th></tr><tr>'
row_counter = first.weekday()

for i in range(first.weekday()):
	result_list += '<td></td>'

while first <= last:
	if row_counter == 7:
		result_list += "</tr><tr>"
		row_counter = 0
	if str(first.strftime("%Y-%m-%d")) in reservated_dates:
		result_list += '<td><font color="red">' + str(first.strftime("%d")) + "</font></td>"
	else:
		result_list += '<td><font color="green">' + str(first.strftime("%d")) + "</font></td>"

	row_counter += 1
	first = first + timedelta(days=1)

result_list += '</tr></table>'

print (result_list)

connection.close()
