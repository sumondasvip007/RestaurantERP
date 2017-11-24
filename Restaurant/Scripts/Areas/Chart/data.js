
//Bar Chart
var dataSource = [{
    day: "Monday",
    oranges: 3
}, {
    day: "Tuesday",
    oranges: 2
}, {
    day: "Wednesday",
    oranges: 3
}, {
    day: "Thursday",
    oranges: 4
}, {
    day: "Friday",
    oranges: 6
}, {
    day: "Saturday",
    oranges: 11
}, {
    day: "Sunday",
    oranges: 4
}];
////////


var dataSourceForDoughnut = [{
    region: "Asia",
    val: 4119626293
}, {
    region: "Africa",
    val: 1012956064
}, {
    region: "Northern America",
    val: 344124520
}, {
    region: "Latin America and the Caribbean",
    val: 590946440
}, {
    region: "Europe",
    val: 727082222
}, {
    region: "Oceania",
    val: 35104756
}];


var dataSourceForLine = [{
    country: "USA",
    hydro: 59.8,
    oil: 937.6,
    gas: 582,
    coal: 564.3,
    nuclear: 187.9
}, {
    country: "China",
    hydro: 74.2,
    oil: 308.6,
    gas: 35.1,
    coal: 956.9,
    nuclear: 11.3
}, {
    country: "Russia",
    hydro: 40,
    oil: 128.5,
    gas: 361.8,
    coal: 105,
    nuclear: 32.4
}, {
    country: "Japan",
    hydro: 22.6,
    oil: 241.5,
    gas: 64.9,
    coal: 120.8,
    nuclear: 64.8
}, {
    country: "India",
    hydro: 19,
    oil: 119.3,
    gas: 28.9,
    coal: 204.8,
    nuclear: 3.8
}, {
    country: "Germany",
    hydro: 6.1,
    oil: 123.6,
    gas: 77.3,
    coal: 85.7,
    nuclear: 37.8
}];

var types = ["line", "stackedline", "fullstackedline"];


var dataSourceForPie = [{
    country: "Russia",
    area: 12
}, {
    country: "Canada",
    area: 7
}, {
    country: "USA",
    area: 7
}, {
    country: "China",
    area: 7
}, {
    country: "Brazil",
    area: 6
}, {
    country: "Australia",
    area: 5
}, {
    country: "India",
    area: 2
}, {
    country: "Others",
    area: 55
}];





var dataSourceForPolar = [{
    arg: 0,
    val: 0
}, {
    arg: 30,
    val: 1.7
}, {
    arg: 45,
    val: 0
}, {
    arg: 60,
    val: 1.7
}, {
    arg: 90,
    val: 0
}, {
    arg: 120,
    val: 1.7
}, {
    arg: 135,
    val: 0
}, {
    arg: 150,
    val: 1.7
}, {
    arg: 180,
    val: 0
}, {
    arg: 210,
    val: 1.7
}, {
    arg: 225,
    val: 0
}, {
    arg: 240,
    val: 1.7
}, {
    arg: 270,
    val: 0
}, {
    arg: 300,
    val: 1.7
}, {
    arg: 315,
    val: 0
}, {
    arg: 330,
    val: 1.7
}, {
    arg: 360,
    val: 0
}];


var dataSourceForSpider = [{
    arg: "USA",
    apples: 4.21,
    grapes: 6.22,
    lemons: 0.8,
    oranges: 7.47
}, {
    arg: "China",
    apples: 3.33,
    grapes: 8.65,
    lemons: 1.06,
    oranges: 5
}, {
    arg: "Turkey",
    apples: 2.6,
    grapes: 4.25,
    lemons: 0.78,
    oranges: 1.71
}, {
    arg: "Italy",
    apples: 2.2,
    grapes: 7.78,
    lemons: 0.52,
    oranges: 2.39
}, {
    arg: "India",
    apples: 2.16,
    grapes: 2.26,
    lemons: 3.09,
    oranges: 6.26
}];





var dataSourceForMonthly = [{
    day: '1',
    temperature: 57
}, {
    day: '2',
    temperature: 58
}, {
    day: '3',
    temperature: 57
}, {
    day: '4',
    temperature: 59
}, {
    day: '5',
    temperature: 63
}, {
    day: '6',
    temperature: 63
}, {
    day: '7',
    temperature: 63
}, {
    day: '8',
    temperature: 64
}, {
    day: '9',
    temperature: 64
}, {
    day: '10',
    temperature: 64
}, {
    day: '11',
    temperature: 69
}, {
    day: '12',
    temperature: 72
}, {
    day: '13',
    temperature: 75
}, {
    day: '14',
    temperature: 78
}, {
    day: '15',
    temperature: 76
}, {
    day: '16',
    temperature: 70
}, {
    day: '17',
    temperature: 65
}, {
    day: '18',
    temperature: 65
}, {
    day: '19',
    temperature: 68
}, {
    day: '20',
    temperature: 70
}, {
    day: '21',
    temperature: 73
}, {
    day: '22',
    temperature: 73
}, {
    day: '23',
    temperature: 75
}, {
    day: '24',
    temperature: 78
}, {
    day: '25',
    temperature: 76
}, {
    day: '26',
    temperature: 76
}, {
    day: '27',
    temperature: 80
}, {
    day: '28',
    temperature: 76
}, {
    day: '29',
    temperature: 75
}, {
    day: '30',
    temperature: 75
}, {
    day: '31',
    temperature: 74
}];