
vid1 = videoinput('winvideo',1)
% Set video input object properties for this application.
set(vid1,'TriggerRepeat',Inf);
vid1.FrameGrabInterval = 1;
set(vid1,'ReturnedColorSpace','rgb');
set(vid1,'FramesPerTrigger', 1000);

% Start acquiring frames.
   start(vid1);
   while(vid1.FramesAcquired<=1000) % Stop after 1000 frames    
        data1 = getdata(vid1,1);
        gray_img = data1(:,:,1);
        figure(1), imshow(gray_img);      
        flushdata(vid1);
   end
   stop(vid1);
   delete(vid1)
    