//ReSharper disable InconsistentNaming  
//ReSharper disable IdentifierTypo

namespace VideoSource.DirectShow
{
    public static class ErrorCodes
    {
        public const uint S_OK = 0x0; //Success.

        public const uint VFW_S_NO_MORE_ITEMS = 0x00040103; //Reached the end of the list; no more items in the list.
                                                            //(Filter developers: The CBasePin::GetMediaType method is expected to return this value.)

        public const uint VFW_S_DUPLICATE_NAME = 0x0004022D; //An attempt to add a filter with a duplicate name succeeded with a modified name.

        public const uint VFW_S_STATE_INTERMEDIATE = 0x00040237; //The state transition is not complete.

        public const uint VFW_S_PARTIAL_RENDER = 0x00040242; //Some of the streams are in an unsupported format.

        public const uint VFW_S_SOME_DATA_IGNORED = 0x00040245; //The file contained some property settings that were not used.

        public const uint VFW_S_CONNECTIONS_DEFERRED = 0x00040246; //Some connections failed and were deferred.

        public const uint VFW_S_RESOURCE_NOT_NEEDED = 0x00040250; //The resource specified is no longer needed.

        public const uint VFW_S_MEDIA_TYPE_IGNORED = 0x00040254; //A GraphEdit(.grf) file was loaded successfully, but at least two pins
                                                                 //were connected using a different media type than the media type stored in the GraphEdit file.
        
        public const uint VFW_S_VIDEO_NOT_RENDERED = 0x00040257; //Cannot play back the video stream: could not find a suitable renderer.

        public const uint VFW_S_AUDIO_NOT_RENDERED = 0x00040258; //Cannot play back the audio stream: could not find a suitable renderer.

        public const uint VFW_S_RPZA = 0x0004025A; //Cannot play back the video stream: format 'RPZA' is not supported.

        public const uint VFW_S_ESTIMATED = 0x00040260; //The value returned had to be estimated.Its accuracy can't be guaranteed.

        public const uint VFW_S_RESERVED = 0x00040263; //This success code is reserved for internal purposes within DirectShow.

        public const uint VFW_S_STREAM_OFF = 0x00040267; //The stream was turned off.

        public const uint VFW_S_CANT_CUE = 0x00040268; //The filter is active, but cannot deliver data.See IMediaFilter::GetState.

        public const uint VFW_S_NOPREVIEWPIN = 0x0004027E; //Preview was rendered throught the Smart Tee filter, because the capture filter
                                                           //does not have a preview pin.

        public const uint VFW_S_DVD_NON_ONE_SEQUENTIAL = 0x00040280; //The current title is not a sequential set of chapters(PGC),
                                                                     //so the timing information might not be continuous.

        public const uint VFW_S_DVD_CHANNEL_CONTENTS_NOT_AVAILABLE = 0x0004028C; //The audio stream does not contain enough information
                                                                                 //to determine the contents of each channel.

        public const uint VFW_S_DVD_NOT_ACCURATE = 0x0004028D; //The seek operation on the DVD was not frame accurate.

        public const uint VFW_E_INVALIDMEDIATYPE = 0x80040200; //The specified media type is invalid.

        public const uint VFW_E_INVALIDSUBTYPE = 0x80040201; //The specified media subtype is invalid.

        public const uint VFW_E_NEED_OWNER = 0x80040202; //This object can only be created as an aggregated object.

        public const uint VFW_E_ENUM_OUT_OF_SYNC = 0x80040203; //The state of the enumerated object has changed and is now inconsistent
                                                               //with the state of the enumerator.Discard any data obtained from previous calls
                                                               //to the enumerator and then update the enumerator by calling the enumerator's Reset method.

        public const uint VFW_E_ALREADY_CONNECTED = 0x80040204; //At least one of the pins involved in the operation is already connected.

        public const uint VFW_E_FILTER_ACTIVE = 0x80040205; //This operation cannot be performed because the filter is active.

        public const uint VFW_E_NO_TYPES = 0x80040206; //One of the specified pins supports no media types.

        public const uint VFW_E_NO_ACCEPTABLE_TYPES = 0x80040207; //There is no common media type between these pins.

        public const uint VFW_E_INVALID_DIRECTION = 0x80040208; //Two pins of the same direction cannot be connected.

        public const uint VFW_E_NOT_CONNECTED = 0x80040209; //The operation cannot be performed because the pins are not connected.

        public const uint VFW_E_NO_ALLOCATOR = 0x8004020A; //No sample buffer allocator is available.

        public const uint VFW_E_RUNTIME_ERROR = 0x8004020B; //A run-time error occurred.

        public const uint VFW_E_BUFFER_NOTSET = 0x8004020C; //No buffer space has been set.

        public const uint VFW_E_BUFFER_OVERFLOW = 0x8004020D; //The buffer is not big enough.

        public const uint VFW_E_BADALIGN = 0x8004020E; //An invalid alignment was specified.

        public const uint VFW_E_ALREADY_COMMITTED = 0x8004020F; //The allocator was not committed.See IMemAllocator::Commit.

        public const uint VFW_E_BUFFERS_OUTSTANDING = 0x80040210; //One or more buffers are still active.

        public const uint VFW_E_NOT_COMMITTED = 0x80040211; //Cannot allocate a sample when the allocator is not active.

        public const uint VFW_E_SIZENOTSET = 0x80040212; //Cannot allocate memory because no size has been set.

        public const uint VFW_E_NO_CLOCK = 0x80040213; //Cannot lock for synchronization because no clock has been defined.

        public const uint VFW_E_NO_SINK = 0x80040214; //Quality messages could not be sent because no quality sink has been defined.

        public const uint VFW_E_NO_INTERFACE = 0x80040215; //A required interface has; //not been implemented.

        public const uint VFW_E_NOT_FOUND = 0x80040216; //An object or name was not found.

        public const uint VFW_E_CANNOT_CONNECT = 0x80040217; //No combination of intermediate filters could be found to make the connection.

        public const uint VFW_E_CANNOT_RENDER = 0x80040218; //No combination of filters could be found to render the stream.

        public const uint VFW_E_CHANGING_FORMAT = 0x80040219; //Could not change formats dynamically.

        public const uint VFW_E_NO_COLOR_KEY_SET = 0x8004021A; //No color key has been set.

        public const uint VFW_E_NOT_OVERLAY_CONNECTION = 0x8004021B; //Current pin connection is not using the IOverlay transport.

        public const uint VFW_E_NOT_SAMPLE_CONNECTION = 0x8004021C; //Current pin connection is not using the IMemInputPin transport.

        public const uint VFW_E_PALETTE_SET = 0x8004021D; //Setting a color key would conflict with the palette already set.

        public const uint VFW_E_COLOR_KEY_SET = 0x8004021E; //Setting a palette would conflict with the color key already set.

        public const uint VFW_E_NO_COLOR_KEY_FOUND = 0x8004021F; //No matching color key is available.

        public const uint VFW_E_NO_PALETTE_AVAILABLE = 0x80040220; //No palette is available.

        public const uint VFW_E_NO_DISPLAY_PALETTE = 0x80040221; //Display does not use a palette.

        public const uint VFW_E_TOO_MANY_COLORS = 0x80040222; //Too many colors for the current display settings.

        public const uint VFW_E_STATE_CHANGED = 0x80040223; //The state changed while waiting to process the sample.

        public const uint VFW_E_NOT_STOPPED = 0x80040224; //The operation could not be performed because the filter is not stopped.

        public const uint VFW_E_NOT_PAUSED = 0x80040225; //The operation could not be performed because the filter is not paused.

        public const uint VFW_E_NOT_RUNNING = 0x80040226; //The operation could not be performed because the filter is not running.

        public const uint VFW_E_WRONG_STATE = 0x80040227; //The operation could not be performed because the filter is in the wrong state.

        public const uint VFW_E_START_TIME_AFTER_END = 0x80040228; //The sample start time is after the sample end time.

        public const uint VFW_E_INVALID_RECT = 0x80040229; //The supplied rectangle is invalid.

        public const uint VFW_E_TYPE_NOT_ACCEPTED = 0x8004022A; //This pin cannot use the supplied media type.

        public const uint VFW_E_SAMPLE_REJECTED = 0x8004022B; //This sample cannot be rendered.

        public const uint VFW_E_SAMPLE_REJECTED_EOS = 0x8004022C; //This sample cannot be rendered because the end of the stream has been reached.

        public const uint VFW_E_DUPLICATE_NAME = 0x8004022D; //An attempt to add a filter with a duplicate name failed.

        public const uint VFW_E_TIMEOUT = 0x8004022E; //A time-out has expired.

        public const uint VFW_E_INVALID_FILE_FORMAT = 0x8004022F; //The file format is invalid.

        public const uint VFW_E_ENUM_OUT_OF_RANGE = 0x80040230; //The list has already been exhausted.

        public const uint VFW_E_CIRCULAR_GRAPH = 0x80040231; //The filter graph is circular.

        public const uint VFW_E_NOT_ALLOWED_TO_SAVE = 0x80040232; //Updates are not allowed in this state.

        public const uint VFW_E_TIME_ALREADY_PASSED = 0x80040233; //An attempt was made to queue a command for a time in the past.

        public const uint VFW_E_ALREADY_CANCELLED = 0x80040234; //The queued command was already canceled.

        public const uint VFW_E_CORRUPT_GRAPH_FILE = 0x80040235; //Cannot render the file because it is corrupt.

        public const uint VFW_E_ADVISE_ALREADY_SET = 0x80040236; //An IOverlay advise link already exists.

        public const uint VFW_E_NO_MODEX_AVAILABLE = 0x80040238; //No full-screen modes are available.

        public const uint VFW_E_NO_ADVISE_SET = 0x80040239; //This advise cannot be canceled because it was not successfully set.

        public const uint VFW_E_NO_FULLSCREEN = 0x8004023A; //Full-screen mode is not available.

        public const uint VFW_E_IN_FULLSCREEN_MODE = 0x8004023B; //Cannot call IVideoWindow methods while in full-screen mode.

        public const uint VFW_E_UNKNOWN_FILE_TYPE = 0x80040240; //The media type of this file is not recognized.

        public const uint VFW_E_CANNOT_LOAD_SOURCE_FILTER = 0x80040241; //The source filter for this file could not be loaded.

        public const uint VFW_E_FILE_TOO_SHORT = 0x80040243; //A file appeared to be incomplete.

        public const uint VFW_E_INVALID_FILE_VERSION = 0x80040244; //The file's version number is invalid.

        public const uint VFW_E_INVALID_CLSID = 0x80040247; //This file is corrupt: it contains an invalid class identifier.

        public const uint VFW_E_INVALID_MEDIA_TYPE = 0x80040248; //This file is corrupt: it contains an invalid media type.

        public const uint VFW_E_SAMPLE_TIME_NOT_SET = 0x80040249; //No time stamp has been set for this sample.

        public const uint VFW_E_MEDIA_TIME_NOT_SET = 0x80040251; //No media time was set for this sample.

        public const uint VFW_E_NO_TIME_FORMAT_SET = 0x80040252; //No media time format was selected.

        public const uint VFW_E_MONO_AUDIO_HW = 0x80040253; //Cannot change balance because audio device is monoaural only.

        public const uint VFW_E_NO_DECOMPRESSOR = 0x80040255; //Cannot play back the video stream: could not find a suitable decompressor.

        public const uint VFW_E_NO_AUDIO_HARDWARE = 0x80040256; //Cannot play back the audio stream: no audio hardware is available,
                                                                //or the hardware is not supported.

        public const uint VFW_E_RPZA = 0x80040259; //Cannot play back the video stream: format 'RPZA' is not supported.

        public const uint VFW_E_PROCESSOR_NOT_SUITABLE = 0x8004025B; //DirectShow cannot play MPEG movies on this processor.

        public const uint VFW_E_UNSUPPORTED_AUDIO = 0x8004025C; //Cannot play back the audio stream: the audio format is not supported.

        public const uint VFW_E_UNSUPPORTED_VIDEO = 0x8004025D; //Cannot play back the video stream: the video format is not supported.

        public const uint VFW_E_MPEG_NOT_CONSTRAINED = 0x8004025E; //DirectShow cannot play this video stream because it falls outside the constrained standard.

        public const uint VFW_E_NOT_IN_GRAPH = 0x8004025F; //Cannot perform the requested function on an object that is not in the filter graph.

        public const uint VFW_E_NO_TIME_FORMAT = 0x80040261; //Cannot access the time format on an object.

        public const uint VFW_E_READ_ONLY = 0x80040262; //Could not make the connection because the stream is read-only and the filter alters the data.

        public const uint VFW_E_BUFFER_UNDERFLOW = 0x80040264; //The buffer is not full enough.

        public const uint VFW_E_UNSUPPORTED_STREAM = 0x80040265; //Cannot play back the file: the format is not supported.

        public const uint VFW_E_NO_TRANSPORT = 0x80040266; //Pins cannot connect because they don't support the same transport.
                                                           //For example, the upstream filter might require the IAsyncReader interface,
                                                           //while the downstream filter requires IMemInputPin.

        public const uint VFW_E_BAD_VIDEOCD = 0x80040269; //The Video CD can't be read correctly by the device or the data is corrupt.

        public const uint VFW_S_NO_STOP_TIME = 0x80040270; //The sample had a start time but not a stop time.
                                                           //In this case, the stop time that is returned is set to the start time plus one.

        public const uint VFW_E_OUT_OF_VIDEO_MEMORY = 0x80040271; //There is not enough video memory at this display resolution and number of colors.
                                                                  //Reducing resolution might help.

        public const uint VFW_E_VP_NEGOTIATION_FAILED = 0x80040272; //The video port connection negotiation process has failed.

        public const uint VFW_E_DDRAW_CAPS_NOT_SUITABLE = 0x80040273; //Either DirectDraw has not been installed or the video card capabilities
                                                                      //are not suitable.Make sure the display is not in 16-color mode.

        public const uint VFW_E_NO_VP_HARDWARE = 0x80040274; //No video port hardware is available, or the hardware is not responding.

        public const uint VFW_E_NO_CAPTURE_HARDWARE = 0x80040275; //No capture hardware is available, or the hardware is not responding.

        public const uint VFW_E_DVD_OPERATION_INHIBITED = 0x80040276; //This user operation is prohibited by DVD content at this time.

        public const uint VFW_E_DVD_INVALIDDOMAIN = 0x80040277; //This operation is not permitted in the current domain.

        public const uint VFW_E_DVD_NO_BUTTON = 0x80040278; //Requested button is not available.

        public const uint VFW_E_DVD_GRAPHNOTREADY = 0x80040279; //DVD-Video playback graph has not been built yet.

        public const uint VFW_E_DVD_RENDERFAIL = 0x8004027A; //DVD-Video playback graph building failed.

        public const uint VFW_E_DVD_DECNOTENOUGH = 0x8004027B; //DVD-Video playback graph could not be built due to insufficient decoders.

        public const uint VFW_E_DDRAW_VERSION_NOT_SUITABLE = 0x8004027C; //The DirectDraw version number is not suitable.
                                                                         //Make sure to install DirectX 5 or higher.

        public const uint VFW_E_COPYPROT_FAILED = 0x8004027D; //Copy protection could not be enabled.

        public const uint VFW_E_TIME_EXPIRED = 0x8004027F; //Seek command timed out.

        public const uint VFW_E_DVD_WRONG_SPEED = 0x80040281; //The operation cannot be performed at the current playback speed.

        public const uint VFW_E_DVD_MENU_DOES_NOT_EXIST = 0x80040282; //The specified DVD menu does not exist.

        public const uint VFW_E_DVD_CMD_CANCELLED = 0x80040283; //The specified command was canceled or no longer exists.

        public const uint VFW_E_DVD_STATE_WRONG_VERSION = 0x80040284; //The DVD state information contains the wrong version number.

        public const uint VFW_E_DVD_STATE_CORRUPT = 0x80040285; //The DVD state information is corrupted.

        public const uint VFW_E_DVD_STATE_WRONG_DISC = 0x80040286; //The DVD state information is from another disc and not the current disc.

        public const uint VFW_E_DVD_INCOMPATIBLE_REGION = 0x80040287; //The region is not compatible with the drive.

        public const uint VFW_E_DVD_NO_ATTRIBUTES = 0x80040288; //The requested attributes do not exist.

        public const uint VFW_E_DVD_NO_GOUP_PGC = 0x80040289; //The operation cannot be performed because no GoUp program chain(PGC) is available.

        public const uint VFW_E_DVD_LOW_PARENTAL_LEVEL = 0x8004028A; //The operation is prohibited because the parental level is too low.

        public const uint VFW_E_DVD_NOT_IN_KARAOKE_MODE = 0x8004028B; //The DVD Navigator is not in karaoke mode.

        public const uint VFW_E_FRAME_STEP_UNSUPPORTED = 0x8004028E; //Frame stepping is not supported.

        public const uint VFW_E_DVD_STREAM_DISABLED = 0x8004028F; //The requested stream is disabled.

        public const uint VFW_E_DVD_TITLE_UNKNOWN = 0x80040290; //The operation requires a title number, but there is no current title.
                                                                //This error can occur when the DVD Navigator is not in the Title domain
                                                                //or the Video Title Set Menu(VTSM) domain.

        public const uint VFW_E_DVD_INVALID_DISC = 0x80040291; //The specified path is not a valid DVD disc.

        public const uint VFW_E_DVD_NO_RESUME_INFORMATION = 0x80040292; //The Resume operation could not be completed, because there is no resume information.

        public const uint VFW_E_PIN_ALREADY_BLOCKED_ON_THIS_THREAD = 0x80040293; //Pin is already blocked on the calling thread.

        public const uint VFW_E_PIN_ALREADY_BLOCKED = 0x80040294; //Pin is already blocked on another thread.

        public const uint VFW_E_CERTIFICATION_FAILURE = 0x80040295; //Use of this filter is restricted by a software key.The application must unlock the filter.

        public const uint VFW_E_VMR_NOT_IN_MIXER_MODE = 0x80040296; //The Video Mixing Renderer(VMR) is not in mixing mode.
                                                                    //Call IVMRFilterConfig::SetNumberOfStreams (VMR-7) or IVMRFilterConfig9::SetNumberOfStreams (VMR-9).

        public const uint VFW_E_VMR_NO_AP_SUPPLIED = 0x80040297; //The application has not yet provided the VMR filter with a valid allocator-presenter object.

        public const uint VFW_E_VMR_NO_DEINTERLACE_HW = 0x80040298; //The VMR could not find any de-interlacing hardware on the current display device.

        public const uint VFW_E_VMR_NO_PROCAMP_HW = 0x80040299; //The VMR could not find any hardware that supports ProcAmp controls on the current display device.

        public const uint VFW_E_DVD_VMR9_INCOMPATIBLEDEC = 0x8004029A; //The hardware decoder uses video port extensions(VPE), which are not compatible with the VMR-9 filter.

        public const uint VFW_E_NO_COPP_HW = 0x8004029B; //The current display device does not support Content Output Protection Protocol(COPP);
                                                         //or the VMR has not connected to a display device yet.

        public const uint VFW_E_BAD_KEY = 0x800403F2; //A registry entry is corrupt.

        public const uint VFW_E_DVD_NONBLOCKING = 0x8004029C; //The DVD navigator cannot complete the requested operation, because another operation is still pending.

        public const uint VFW_E_DVD_TOO_MANY_RENDERERS_IN_FILTER_GRAPH = 0x8004029D; //The DVD Navigator cannot build the DVD playback graph
                                                                                     //because the graph contains more than one video renderer.

        public const uint VFW_E_DVD_NON_EVR_RENDERER_IN_FILTER_GRAPH = 0x8004029E; //The DVD Navigator cannot add the Enhanced Video Renderer(EVR) filter
                                                                                   //to the filter graph because the graph already contains a video renderer.

        public const uint VFW_E_DVD_RESOLUTION_ERROR = 0x8004029F; //DVD Video output is not at a proper resolution.

        public const uint VFW_E_CODECAPI_LINEAR_RANGE = 0x80040310; //The specified codec parameter has a linear range, not an enumerated list.

        public const uint VFW_E_CODECAPI_ENUMERATED = 0x80040311; //The specified codec parameter has an enumerated range of values, not a linear range.

        public const uint VFW_E_CODECAPI_NO_DEFAULT = 0x80040313; //The specified codec parameter does not have a default value.

        public const uint VFW_E_CODECAPI_NO_CURRENT_VALUE = 0x80040314; //The specified codec parameter does not have a current value.

        public const uint E_PROP_ID_UNSUPPORTED = 0x80070490; //The specified property identifier is not supported.

        public const uint E_PROP_SET_UNSUPPORTED = 0x80070492; //The specified property set is not supported.

        public const uint S_WARN_OUTPUTRESET = 0x00009DD4; //The rendering portion of the graph was deleted.The application must rebuild it.

        public const uint E_NOTINTREE = 0x80040400; //The object is not contained in the timeline.

        public const uint E_RENDER_ENGINE_IS_BROKEN = 0x80040401; //Operation failed because project was not rendered successfully.

        public const uint E_MUST_INIT_RENDERER = 0x80040402; //Render engine has not been initialized.

        public const uint E_NOTDETERMINED = 0x80040403; //Cannot determine requested value.

        public const uint E_NO_TIMELINE = 0x80040404; //There is no timeline object.
    }
}