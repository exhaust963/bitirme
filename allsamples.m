function curs=allsamples(isim,conn)
isim=strcat('''',isim,'''');
isim=sprintf('%s',isim);
curs=select(conn,strcat('Select * FROM al�nan_dersler,ogrenci Where ogrenci.OgrenciIsim=',isim));
end

