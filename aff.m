layer = 'conv_1';
channels = 1:8;

I = deepDreamImage(net,layer,channels, ...
    'PyramidLevels',3, ...
    'Verbose',0);

figure
for i = 1:8
    subplot(4,4,i)
    imshow(I(:,:,:,i))
end