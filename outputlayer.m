function image = outputlayer(net,im,name)
im = imresize(im,net.Layers(1).InputSize(1:2));
act = activations(net,im,name,'OutputAs','channels');
act = reshape(act,size(act,1),size(act,2),1,size(act,3));
act_scaled = mat2gray(act);
image=montage(act_scaled);
end
