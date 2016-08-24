// by firefoxmmx
var script = document.createElement("script");
script.type = "text/javascript";
//script.src = "plugins/jQuery/jquery-1.11.2.min.js";
document.getElementsByTagName('head')[0].appendChild(script);


setTimeout(function () {

    var i = document.getElementById("Mychart").offsetWidth;
    document.getElementById("Mychart").style.width = i + "px";
    document.getElementById("Mychart1").style.width = i + "px";
    document.getElementById("Mychart2").style.width = i + "px";
    document.getElementById("Mychart3").style.width = i + "px";
    document.getElementById("Mychart4").style.width = i + "px";
    document.getElementById("Mychart5").style.width = i + "px";
});
setTimeout(function () {
       var x = document.getElementById("TDMychart").offsetWidth;
        document.getElementById("TDMychart").style.width = x + "px";
        document.getElementById("TDMychart1").style.width = x + "px";
        document.getElementById("TDMychart2").style.width = x + "px";
        document.getElementById("TDMychart3").style.width = x + "px";
        document.getElementById("TDMychart4").style.width = x + "px";
        document.getElementById("TDMychart5").style.width = x + "px";
});

setTimeout(function () {
    Mchart("chartJD", "Mychart");
    });

setTimeout(function () {
    $("#chartJD").click(function () {
        Mchart("chartJD", "Mychart");
    });
});//个人接待

setTimeout(function () {
    $("#chartCPH").click(function () {
        Mchart("chartCPH", "Mychart1");
       });
});//个人CPH

setTimeout(function () {
    $("#chartZHL").click(function () {
        Mchart("chartZHL", "Mychart2");
    });
});//个人转化率

setTimeout(function () {
    $("#chartFXY").click(function () {
        Mchart("chartFXY", "Mychart3");
    });
});//个人首次响应

setTimeout(function () {
    $("#chartAXY").click(function () {
        Mchart("chartAXY", "Mychart4");
    });
});//个人平均响应

setTimeout(function () {
    $("#chartAJD").click(function () {
        Mchart("chartAJD", "Mychart5");
    });
});//个人平均接待
setTimeout(function () {

        TDMchart("TDchartJD", "TDMychart");
    });

setTimeout(function () {
    $("#TDchartJD").click(function () {
        TDMchart("TDchartJD", "TDMychart");
    });
});//组接待

setTimeout(function () {
    $("#TDchartCPH").click(function () {
        TDMchart("TDchartCPH", "TDMychart1");
    });
});//组CPH

setTimeout(function () {
    $("#TDchartZHL").click(function () {
        TDMchart("TDchartZHL", "TDMychart2");
    });
});//组转化率

setTimeout(function () {
    $("#TDchartFXY").click(function () {
        TDMchart("TDchartFXY", "TDMychart3");
    });
});//组首次响应

setTimeout(function () {
    $("#TDchartAXY").click(function () {
        TDMchart("TDchartAXY", "TDMychart4");
    });
});//组平均响应

setTimeout(function () {
    $("#TDchartAJD").click(function () {
        TDMchart("TDchartAJD", "TDMychart5");
    });
});//组平均接待


function Mchart(type,chart) {
    var myChart = echarts.init(document.getElementById(chart));
    var label = document.getElementById('Nlab').innerText;
    var tp = document.getElementById(type).innerText;
    // 指定图表的配置项和数据
    var date = [];
    var data = [];
    var data1 = [];
    var arr = new Array();
    var arr1 = new Array();
    var arr2 = new Array();

    de = $.ajax({
        type: "GET",
        data: { "miliao":label,"type": "日期","TD":"0" },
        url: "ajax/taobao/tbyg.ashx",
        async: false
    });

    da = $.ajax({
        type: "GET",
        data: { "miliao":label,"type":tp,"TD":"0" },
        url: "ajax/taobao/tbyg.ashx",
        async: false
    });

    da1 = $.ajax({
        type: "GET",
        data: { "miliao":label,"type":tp,"TD":"1"},
        url: "ajax/taobao/tbyg.ashx",
        async: false
    });

    arr = de.responseText.split(",");
    arr1 = da.responseText.split(",");
    arr2 = da1.responseText.split(",");

    for (var i = 0; i < arr.length; i++) {
        date.push(arr[i]);
    };

    for (var i = 0; i < arr1.length; i++) {
        data.push(arr1[i]);
    };
    for (var i = 0; i < arr2.length; i++) {
        data1.push(arr2[i]);
    };

    var option = {
        title: {
            text: ''
        },
        tooltip: {
            trigger: 'axis',
            axisPointer: {
                type: 'line'
            }
        },
        toolbox: {
            feature: {
                magicType: { show: true, type: ['line', 'bar'] },
                restore: { show: true },
            },
            right: '20'
        },
        legend: {
            data: [tp + '数据', '团队' +tp+ '均值']
        },
        xAxis: {
            data: date,
            axisLabel: { interval: 3 }
        },
        yAxis: {},
        series: [{
            name: tp + '数据',
            type: 'bar',
            data: data,
            markPoint: {
                data: [
                    { type: 'max', name: '最大值' },
                    { type: 'min', name: '最小值' }
                ],
                symbolSize: 70,
                itemStyle: {
                    normal: {
                        color: '#2f4554'
                    }
                },
            },
            markLine: {
                data: [
                    { type: 'average', name: '平均值' }
                ],
                lineStyle: {
                    normal: {
                        color: '#00CD00'
                    }
                },
            }
        },
        {
            name: '团队'+tp + '均值',
            type: 'line',
            data: data1,
            lineStyle: { normal: { color: 'rgb(0,0,255)' } },
        }
        ]

    };
    // 使用刚指定的配置项和数据显示图表。
    myChart.setOption(option);
};

function TDMchart(type, chart) {
    {
        var myChart = echarts.init(document.getElementById(chart));
        var tp = document.getElementById(type).innerText;
        // 指定图表的配置项和数据
        var date = [];
        var data = [];
        var data1 = [];
        var arr = new Array();
        var arr1 = new Array();
        var arr2 = new Array();

        de = $.ajax({
            type: "GET",
            data: { "type": tp, "TD": "1" },
            url: "Ajax/taobao/tbtd.ashx",
            async: false
        });

        da = $.ajax({
            type: "GET",
            data: { "type": tp, "TD": "0" },
            url: "Ajax/taobao/tbtd.ashx",
            async: false
        });

        da1 = $.ajax({
            type: "GET",
            data: {"type": tp, "TD": "2" },
            url: "Ajax/taobao/tbtd.ashx",
            async: false
        });

        arr = de.responseText.split(",");
        arr1 = da.responseText.split(",");
        arr2 = da1.responseText.split(",");

        for (var i = 0; i < arr.length; i++) {
            date.push(arr[i]);
        };

        for (var i = 0; i < arr1.length; i++) {
            data.push(arr1[i]);
        };
        for (var i = 0; i < arr2.length; i++) {
            data1.push(arr2[i]);
        };

        var option = {
            title: {
                text: ''
            },
            tooltip: {
                trigger: 'axis',
                axisPointer: {
                    type: 'line'
                }
            },
            toolbox: {
                feature: {
                    magicType: { show: true, type: ['line', 'bar'] },
                    restore: { show: true },
                },
                right: '20'
            },
            legend: {
                data: [tp + '数据', '团队' + tp + '均值']
            },
            xAxis: {
                data: date,
                axisLabel: { interval: 0 }
            },
            yAxis: {},
            series: [{
                name: tp + '数据',
                type: 'bar',
                data: data,
                barMaxWidth: 80,
            },
            {
                name: '团队' + tp + '均值',
                type: 'line',
                data: data1,
                lineStyle: { normal: { color: 'rgb(0,0,255)' } },
            }
            ]

        };
        // 使用刚指定的配置项和数据显示图表。
        myChart.setOption(option);
    }
}



