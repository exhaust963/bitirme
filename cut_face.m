function image=cut_face(im)
faceDetector=vision.CascadeObjectDetector();
bbox=step(faceDetector,im);
for i= 1:size(bbox,1)
rectangle('Position', bbox(i,:), 'LineWidth', 3, 'LineStyle', '-', 'EdgeColor', 'r');
end
bbox(1,1) = bbox(1,1) - 150;  %SOL
bbox(1,2) = bbox(1,2) - 150;  %ASA
bbox(1,3) = bbox(1,3) + 200;  %SAG
bbox(1,4) = bbox(1,4) + 200;  %YUKARI
for i=1:size(bbox,1)
   image=imcrop(im,bbox(i,:));
end
imwrite(image,'leo2_sonn.jpg');
end
