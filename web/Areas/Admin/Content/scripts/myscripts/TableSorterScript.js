function TableSorter(tablename,headers,pagername){

    // A-Z, Z-A sıralama
    var tableSortHeaders = new Array;
   
    $.each(headers.split(','),function(index,value){
        tableSortHeaders[value]={sorter:false};
    });

    if (pagername != null) {
        $("#" + tablename).tablesorter({
            widgets: ['zebra'],
            headers: tableSortHeaders
        }).tablesorterPager({ container: $('#' + pagername) });
    }
    else {
        $("#" + tablename).tablesorter({
            widgets: ['zebra'],
            headers: tableSortHeaders
        });
    }
    //$("#" + tablename).tableFilter({
    //    filteringRows: function (filterStates) {
    //        //console.log(filterStates);
    //    }
    //});
}