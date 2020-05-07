from datetime import datetime
from random import randint
import csv

MAX_PRODUCTS = 15
MAX_ORDERS = 30

TIMESTAMP_BG2019 = 1546300800
TIMESTAMP_END2019 = 1577836799

def gen_datetime():
    secs = randint(TIMESTAMP_BG2019, TIMESTAMP_END2019)
    return datetime.fromtimestamp(secs).strftime("%Y-%m-%d")

def write_on_csv_file(destino,text):
    with open(destino+".csv", 'a', encoding='utf8' ) as f:
        f.write(text+"\n")

def generate_Products():
 for i in range(1,MAX_PRODUCTS+1):
        cod = str(i)
        nombre = "prod"+cod
        precio = randint(1,100)
        csvString =  ("{0},{1},{2},{3}").format(
            cod, nombre, int(precio),
            )
        write_on_csv_file("product",csvString)

def generate_Order():
    for i in range(1,MAX_ORDERS+1):
        n_order = i
        date = "'"+gen_datetime()+"'"
        quantity = randint(1,5)
        cod_prd = randint(1, MAX_PRODUCTS)
        csvString = ("{0},{1},{2},{3}").format(
            n_order, date, quantity, cod_prd
        )
        write_on_csv_file("order",csvString)
    

#generate_Order()

