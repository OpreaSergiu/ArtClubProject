#!/usr/bin/python3

from reportsmodule import *

SQLCommand = """SELECT UserName, EventId, EventName, Amount, FORMAT(Date,'yyyy-MM'), FORMAT(Month,'yyyy-MM') FROM CostsModels WHERE FORMAT(Month,'yyyy-MM') = '%s' ORDER BY Date"""

SQLCommand1 = """SELECT UserName, Member, Amount, FORMAT(Date,'yyyy-MM'), FORMAT(Month,'yyyy-MM') FROM PaymentsModels WHERE FORMAT(Month,'yyyy-MM') = '%s' ORDER BY Date"""

if len(sys.argv) > 1:
    target_month = sys.argv[1]
else:
    target_month = ""

print (str(sys.argv))

cursor.execute(SQLCommand % target_month) 

results = cursor.fetchone() 

workbook = xlsxwriter.Workbook('C:\\Users\\Sergiu\\source\\repos\\ArtClubProject\\ArtClub\\ArtClub\\ReportingFolder\\Income_Rerpot.xlsx')

worksheet2 = workbook.add_worksheet('Summary')
worksheet = workbook.add_worksheet('Expenses')

total_costs = 0
total_payments = 0

# Set column width.
worksheet.set_column(0, 0, 20)
worksheet.set_column(1, 1, 12)
worksheet.set_column(2, 2, 12)
worksheet.set_column(3, 4, 12)
worksheet.set_column(4, 4, 12)
worksheet.set_column(5, 5, 12)

# Format
f_text_center_tahoma_8 = workbook.add_format(text_center_tahoma_8)
f_headers_format = workbook.add_format(headers_format)
f_text_left_tahoma_8 = workbook.add_format(text_left_tahoma_8)

# Header list.
headers = ['User', 'Event Id', 'Event Name', 'Amount', 'Date', 'Month']

# Writing header.
writeHeader(worksheet, 0, 0, headers, format=f_headers_format)

row = 1

while results:
    worksheet.write(row, 0, str(results[0]), f_text_center_tahoma_8)
    worksheet.write(row, 1, str(results[1]), f_text_center_tahoma_8)
    worksheet.write(row, 2, str(results[2]), f_text_center_tahoma_8)
    worksheet.write(row, 3, str(results[3]), f_text_left_tahoma_8)
    worksheet.write(row, 4, str(results[4]), f_text_center_tahoma_8)
    worksheet.write(row, 5, str(results[5]), f_text_center_tahoma_8)

    total_costs += float(results[3])

    row += 1

    results = cursor.fetchone() 

#-------------------------------------------------------------

cursor.execute(SQLCommand1 % target_month) 

results = cursor.fetchone() 

worksheet1 = workbook.add_worksheet('Income')

# Set column width.
worksheet1.set_column(0, 0, 20)
worksheet1.set_column(1, 1, 12)
worksheet1.set_column(2, 2, 12)
worksheet1.set_column(3, 4, 12)
worksheet1.set_column(4, 4, 12)

# Format
f_text_center_tahoma_8 = workbook.add_format(text_center_tahoma_8)
f_headers_format = workbook.add_format(headers_format)
f_text_left_tahoma_8 = workbook.add_format(text_left_tahoma_8)
f_percent = workbook.add_format(percent)

# Header list.
headers = ['User', 'Member', 'Amount', 'Date', 'Month']

# Writing header.
writeHeader(worksheet1, 0, 0, headers, format=f_headers_format)

row = 1

while results:
    worksheet1.write(row, 0, str(results[0]), f_text_center_tahoma_8)
    worksheet1.write(row, 1, str(results[1]), f_text_center_tahoma_8)
    worksheet1.write(row, 2, str(results[2]), f_text_left_tahoma_8)
    worksheet1.write(row, 3, str(results[3]), f_text_center_tahoma_8)
    worksheet1.write(row, 4, str(results[4]), f_text_center_tahoma_8)

    total_payments += float(results[2])

    row += 1

    results = cursor.fetchone() 

# Header list.
headers = ['Total Costs', 'Total Payments', 'Profit']

# Writing header.
writeHeader(worksheet2, 0, 0, headers, format=f_headers_format)

worksheet2.set_column(0, 0, 20)
worksheet2.set_column(1, 1, 20)
worksheet2.set_column(2, 2, 20)

row = 1

worksheet2.write(row, 0, total_costs, f_text_left_tahoma_8)
worksheet2.write(row, 1, total_payments, f_text_left_tahoma_8)
worksheet2.write(row, 2, total_payments - total_costs, f_text_left_tahoma_8)


try:
    h11 = float(total_costs)/float(total_payments + total_costs)
except ZeroDivisionError:
    h11 = 0

try:
    h12 = float(total_payments)/float(total_payments + total_costs)
except ZeroDivisionError:
    h12 = 0

worksheet2.write('C20', h11 , f_percent)
worksheet2.write('C21', h12 , f_percent)

# Creates the units chart as type pie.
units_chart = workbook.add_chart({'type': 'pie'})

# Setting series and customer colors.
units_chart.add_series({
    'name': 'Summary Chart',
    'categories': ['Summary', 0, 0, 0, 1],
    'values': ['Summary', 19, 2, 21, 2],
    'data_labels': {'value': True , 'leader_lines': True},
    'data_labels': {'percentage': True},
    'points': [
        {'fill': {'color': '#8989E4'}},
        {'fill': {'color': '#892E5B'}},
    ],

})

# Adds black border to units_chart.
units_chart.set_chartarea({
    'border': {'color': 'black'},

})
# Add a title.
units_chart.set_title({'name': 'Summary Chart' , 'name_font': {'name': 'Arial', 'size' : 9, 'color': '#0054AC', 'position' : 'center'},})

units_chart.set_rotation(90)

# Insert the chart into the worksheet.
worksheet2.insert_chart('B10', units_chart , {'x_scale': 0.65, 'y_scale': 1.18})


connection.close()

workbook.close()