import { SxProps, Theme, Typography } from "@mui/material";
import { motion } from "framer-motion";

interface AnimatedTypographyProps {
  content : string,
  delay: number,
  sx : SxProps<Theme>
}

export const AnimatedBody : React.FC<AnimatedTypographyProps> = ({ content, delay, sx }) => {
  return (
    <Typography
      sx={sx}
      variant="body1"
      component={motion.div}
      initial={{ opacity: 0, scale: 0.5 }}
      animate={{ opacity: 1, scale: 1,  }}
      transition={{ duration: 0.5, delay: delay }}
    >
      {content}
    </Typography>
  )
}

export const AnimatedHeader : React.FC<AnimatedTypographyProps> = ({ content, delay, sx }) => {
  return (
    <Typography
      sx={sx}
      variant="h2"
      component={motion.div}
      initial={{ opacity: 0, scale: 0.5 }}
      animate={{ opacity: 1, scale: 1,  }}
      transition={{ duration: 0.5, delay: delay }}
    >
      {content}
    </Typography>
  )
}

export const AnimatedSubHeader : React.FC<AnimatedTypographyProps> = ({ content, delay, sx }) => {
  return (
    <Typography
      sx={sx}
      variant="h5"
      component={motion.div}
      initial={{ opacity: 0, scale: 0.5 }}
      animate={{ opacity: 1, scale: 1,  }}
      transition={{ duration: 0.5, delay: delay }}
    >
      {content}
    </Typography>
  )
}
