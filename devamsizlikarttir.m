function devamsizlikarttir(isim,conn)
isim=string(isim);
isim=strcat('''',isim,'''');
isim=sprintf('%s',isim);
devam=select(conn,strcat('Select OgrenciDevams�zl�k From ogrenci Where ogrenci.OgrenciIsim=',isim));
devam=table2array(devam);
devam(1)=devam(1)+1;
devam=string(devam(1));
a=strcat('UPDATE ogrenci SET OgrenciDevams�zl�k=',devam);
b=strcat(' Where ogrenci.OgrenciIsim=',isim);
c=strcat(a,b);
fetch(conn,c);
end