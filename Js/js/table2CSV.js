jQuery.fn.table2CSV = function(options) {
    var options = jQuery.extend({
        separator: '~',
        header: [],
        delivery: 'popup' // popup, value
    },
    options);

    var csvData = [];
    var headerArr = [];
    var el = this;

    //header
    var numCols = options.header.length;
    var tmpRow = []; // construct header avalible array

    if (numCols > 0) {
        for (var i = 0; i < numCols; i++) {
            tmpRow[tmpRow.length] = options.header[i];//formatData(options.header[i]);
        }
    } else {
        $(el).filter(':visible').find('th').each(function() {
        if(($(this).text())!='')
        {
            if ($(this).css('display') != 'none') tmpRow[tmpRow.length] = formatData($(this).text());
        }    
        });
    }

    row2CSV(tmpRow);

    // actual data
    $(el).find('tr').each(function() {
        var tmpRow = [];
        $(this).filter(':visible').find('.MyTD').each(function() {
            if ($(this).css('display') != 'none') tmpRow[tmpRow.length] = $(this).text();//formatData($(this).text());
        });
        row2CSV(tmpRow);
    });
    if (options.delivery == 'popup') {
        var mydata = csvData.join('\n');
        return popup(mydata);
    } else {
        var mydata = csvData.join('\n');
        return mydata;
    }

    function row2CSV(tmpRow) {
        var tmp = tmpRow.join('') // to remove any blank rows
        // alert(tmp);
        if (tmpRow.length > 0 && tmp != '') {
            var mystr = tmpRow.join(options.separator);
            csvData[csvData.length] = mystr;
        }
    }
    function formatData(input) {
        // replace " with “
        //var regexp = new RegExp(/["]/g);
        //var output = input.replace(regexp, "“");
        //HTML
        //var regexp = new RegExp(/\<[^\<]+\>/g);
        //var output = output.replace(regexp, "");
        //var output;
        //if (output == "") return '';
        return input;
    }
   
    
    function popup(data) {
        var generator = window.open('', 'csv', 'height=400,width=600');
        generator.document.write('<html><head><title>Copy text and make notepad file</title>');
        generator.document.write('</head><body >');
        generator.document.write('<textArea style=font-family:Cambria;color:red; readonly=true cols=70 rows=15 wrap="off" >');
        generator.document.write(data);
        generator.document.write('</textArea>');
        generator.document.write('<br /><img src=images/pointright.gif /><span style=font-family:Cambria; ><ul><li>Try to submit once or twice and if again runtime error occurs then copy above text (i.e. text in red color) and make the notepad (i.e. .txt) file.</li><li>The content of this data file is used for import option.</li><li>Still have any query please contact anand.deshpande@mahyco.com or call at dawalwadi ext.4784</li></ul>');
        generator.document.write('</body></html>');
        generator.document.close();
        return true;
    }
};