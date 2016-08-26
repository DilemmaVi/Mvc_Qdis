// by firefoxmmx
var script = document.createElement("script");
script.type = "text/javascript";
document.getElementsByTagName('head')[0].appendChild(script);

setTimeout(function () {
    var x = document.getElementById("TDMychart").offsetWidth;
    document.getElementById("TDMychart").style.width = x + "px";
    document.getElementById("TDMychart1").style.width = x + "px";
    document.getElementById("TDMychart2").style.width = x + "px";
    document.getElementById("TDMychart3").style.width = x + "px";
    document.getElementById("TDMychart4").style.width = x + "px";
    document.getElementById("TDMychart5").style.width = x + "px";
});//团队图表固定
setTimeout(function () {
    var x = document.getElementById("HMychart").offsetWidth;
    document.getElementById("HMychart").style.width = x + "px";
    document.getElementById("HMychart1").style.width = x + "px";
    document.getElementById("HMychart2").style.width = x + "px";
    document.getElementById("HMychart3").style.width = x + "px";
    document.getElementById("HMychart4").style.width = x + "px";
    document.getElementById("HMychart5").style.width = x + "px";
});//管理图表固定
setTimeout(function () {
    HMchart("HchartJD", "HMychart");
});

setTimeout(function () {
    $("#HchartJD").click(function () {
        HMchart("HchartJD", "HMychart");
    });
});//个人接待(GL)

setTimeout(function () {
    $("#DDL_yg").change(function () {
        HMchart("HchartJD", "HMychart");
    });
});//个人接待(GL)

setTimeout(function () {
    $("#HchartCPH").click(function () {
        HMchart("HchartCPH", "HMychart1");
    });
});//个人CPH(GL)

setTimeout(function () {
    $("#HchartZHL").click(function () {
        HMchart("HchartZHL", "HMychart2");
    });
});//个人转化率(GL)

setTimeout(function () {
    $("#HchartFXY").click(function () {
        HMchart("HchartFXY", "HMychart3");
    });
});//个人首次响应(GL)

setTimeout(function () {
    $("#HchartAXY").click(function () {
        HMchart("HchartAXY", "HMychart4");
    });
});//个人平均响应(GL)

setTimeout(function () {
    $("#HchartAJD").click(function () {
        HMchart("HchartAJD", "HMychart5");
    });
});//个人平均接待(GL)
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

function HMchart(type, chart) {
    var myChart = echarts.init(document.getElementById(chart));
    var DropDownList1 = document.getElementById("DDL_yg");
    var DropDownList1_Index = DropDownList1.selectedIndex;
    var label = DropDownList1.options[DropDownList1_Index].text;
    var tp = document.getElementById(type).innerText;
    // 指定图表的配置项和数据
    var date = [];
    var data = [];
    var data1 = [];
    var arr = new Array();
    var arr1 = new Array();
    var arr2 = new Array();

    mz = $.ajax({
        type: "GET",
        data: { "name": label },
        url: "/Ajax/miliao.ashx",
        async: false
    });

    var miliao = mz.responseText;

    de = $.ajax({
        type: "GET",
        data: { "miliao": miliao, "type": "日期", "TD": "0" },
        url: "/Ajax/ajax.ashx",
        async: false
    });

    da = $.ajax({
        type: "GET",
        data: { "miliao": miliao, "type": tp, "TD": "0" },
        url: "/Ajax/ajax.ashx",
        async: false
    });

    da1 = $.ajax({
        type: "GET",
        data: { "miliao": miliao, "type": tp, "TD": "1" },
        url: "/Ajax/ajax.ashx",
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
            axisLabel: { interval: 3 }
        },
        yAxis: {},
        series: [{
            name: tp + '数据',
            type: 'bar',
            data: data,
            animationDelay: function (idx) {
                return idx * 30;
            },
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
            name: '团队' + tp + '均值',
            type: 'line',
            data: data1,
            animationDelay: function (idx) {
                return idx * 10 + 100;
            },
            lineStyle: { normal: { color: 'rgb(0,0,255)' } },
        }
        ],
        animationEasing: 'elasticOut',
        animationDelayUpdate: function (idx) {
            return idx * 30;
        }
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
            url: "/Ajax/TDchart.ashx",
            async: false
        });

        da = $.ajax({
            type: "GET",
            data: { "type": tp, "TD": "0" },
            url: "/Ajax/TDchart.ashx",
            async: false
        });

        da1 = $.ajax({
            type: "GET",
            data: { "type": tp, "TD": "2" },
            url: "/Ajax/TDchart.ashx",
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
setTimeout(function () {
    $("#DDL_zg").change(function () {
        $("#DDL_zz option").remove();
        var zuzhang = $('#DDL_zg option:selected').val();
        var arr = new Array();
        da = $.ajax({
            type: "GET",
            data: { "zuzhang": zuzhang, "tp": "0" },
            url: "/Ajax/DDlist.ashx",
            async: false
        });
        arr = da.responseText.split(",");
        var option = $("<option >" + "所有组长" + "</option>");
        $(option).val("所有组长");
        $("#DDL_zz").append(option);
        for (var i = 0; i < arr.length; i++) {
            var newOption = $("<option >" + arr[i] + "</option>");
            $(newOption).val(arr[i]);
            $("#DDL_zz").append(newOption);
        };
    });
});//组长筛选
setTimeout(function () {
    $("#DDL_zz").change(function () {
        $("#DDL_yg option").remove();
        var zuzhang = $('#DDL_zz option:selected').val();
        var arr = new Array();
        da = $.ajax({
            type: "GET",
            data: { "zuzhang": zuzhang, "tp": "1" },
            url: "/Ajax/DDlist.ashx",
            async: false
        });
        arr = da.responseText.split(",");
        var option = $("<option >" + "所有员工" + "</option>");
        $(option).val("所有员工");
        $("#DDL_yg").append(option);
        for (var i = 0; i < arr.length; i++) {
            var newOption = $("<option >" + arr[i] + "</option>");
            $(newOption).val(arr[i]);
            $("#DDL_yg").append(newOption);
        };
    });
});//员工筛选