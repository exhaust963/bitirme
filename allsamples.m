function curs=allsamples(isim,conn)
isim=strcat('''',isim,'''');
isim=sprintf('%s',isim);
curs=select(conn,strcat('Select * FROM alýnan_dersler,ogrenci Where ogrenci.OgrenciIsim=',isim));
end

