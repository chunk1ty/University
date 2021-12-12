(function () {
    'use strict';

    var voteSystemResultUrl = $('#vote-system-result-url').val();

    $.ajax({
        type: 'GET',
        url: voteSystemResultUrl,
        contentType: 'application/json',
        success: function (questions) {
            if (questions !== {}) {
                var postTemplateHtml = document.getElementById('question-result-template').innerHTML;
                var postTemplate = Handlebars.compile(postTemplateHtml);
                document.getElementById('root').innerHTML = postTemplate({ questions: questions });

                for (var q = 0; q < questions.length; q++) {
                    var answerNames = [];
                    var answerData = [];

                    for (var a = 0; a < questions[q].Answers.length; a++) {
                        answerNames.push(questions[q].Answers[a].Name);
                        answerData.push({
                            value: questions[q].Answers[a].AnswerCount,
                            name: questions[q].Answers[a].Name
                        });
                    }

                    var chart = echarts.init(document.getElementById('echart-pie-' + q), theme);

                    chart.setOption({
                        tooltip: {
                            trigger: 'item',
                            formatter: "{a} <br/>{b} : {c} ({d}%)"
                        },
                        legend: {
                            x: 'center',
                            y: 'bottom',
                            data: answerNames
                        },
                        toolbox: {
                            show: true,
                            feature: {
                                magicType: {
                                    show: true,
                                    type: ['pie', 'funnel'],
                                    option: {
                                        funnel: {
                                            x: '25%',
                                            width: '50%',
                                            funnelAlign: 'left',
                                            max: 1548
                                        }
                                    }
                                },
                                saveAsImage: {
                                    show: true
                                }
                            }
                        },
                        calculable: true,
                        series: [{
                            name: questions[q].questionName,
                            type: 'pie',
                            radius: '55%',
                            center: ['50%', '48%'],
                            data: answerData
                        }]
                    });
                }
            }
        },
        error: function (ex) {
            alert('Can not find questions!' + ex);
        }
    });
}());